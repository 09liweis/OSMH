using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
using Stripe;

namespace OSMH.Controllers
{
    public class DonationsController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();

        // GET: Donations
        public ActionResult Admin()
        {
            var donations = db.Donations.Include(d => d.Department);
            return View(donations.ToList());
        }

        // GET: Donations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donations donations = db.Donations.Find(id);
            if (donations == null)
            {
                return HttpNotFound();
            }
            return View(donations);
        }

        // GET: Donations/Index
        public ActionResult Index()
        {
            ViewBag.Department_Id = new SelectList(db.Departments, "Id", "Name");
            return View();
        }

        // POST: Donations/MakeDonation
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,Date,Department_Id,Full_Name,Total_Amount,Anonymity")] Donations donations)
        {
            if (ModelState.IsValid)
            {
                donations.Date = DateTime.Now;
                db.Donations.Add(donations);
                db.SaveChanges();

                int donorId = donations.Id;

                return View("StripePayment", new AcceptedDonations { DonorId = donorId });
            }

            ViewBag.Department_Id = new SelectList(db.Departments, "Id", "Name", donations.Department_Id);
            return View(donations);
        }

        // GET: Donations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donations donations = db.Donations.Find(id);
            if (donations == null)
            {
                return HttpNotFound();
            }
            ViewBag.Department_Id = new SelectList(db.Departments, "Id", "Name", donations.Department_Id);
            return View(donations);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Department_Id,Full_Name,Total_Amount,Anonymity")] Donations donations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            ViewBag.Department_Id = new SelectList(db.Departments, "Id", "Name", donations.Department_Id);
            return View(donations);
        }

        // GET: Donations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donations donations = db.Donations.Find(id);
            if (donations == null)
            {
                return HttpNotFound();
            }
            return View(donations);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donations donations = db.Donations.Find(id);
            db.Donations.Remove(donations);
            db.SaveChanges();
            return RedirectToAction("Admin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /* follow tutorial from cmatskas.com/processing-payments-on-your-site-using-stripe-and-net/ */

        [HttpPost]
        public ActionResult StripePayment(AcceptedDonations model)
        {
            if (ModelState.IsValid)
            {
                var token = GetTokenId(model);
                var chargeId = ChargeCustomer(model.DonationAmount, token);
                var donation = db.Donations.Single(d => d.Id == model.DonorId);
                donation.Total_Amount = decimal.Parse(model.DonationAmount.ToString());
                db.Entry(donation).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        private static string GetTokenId(AcceptedDonations model)
        {
            var myToken = new StripeTokenCreateOptions
            {
                Card = new StripeCreditCardOptions
                {
                    Cvc = model.CardCvc,
                    ExpirationMonth = model.CardExpirationMonth.ToString(),
                    ExpirationYear = model.CardExpirationYear.ToString(),
                    Currency = model.Currency,
                    Number = model.CardNumber,
                    Name = model.CardName
                }
            };

            var tokenService = new StripeTokenService();
            var stripeToken = tokenService.Create(myToken);

            return stripeToken.Id;
        }

        private static string ChargeCustomer(decimal price, string tokenId)
        {
            var myCharge = new StripeChargeCreateOptions
            {
                Amount = Convert.ToInt32(price * 100),
                Currency = "cad",
                Description = "Donation to OSMH. ",
                SourceTokenOrExistingSourceId = tokenId //check if source token is right
            };

            var chargeService = new StripeChargeService();
            var stripeCharge = chargeService.Create(myCharge);

            return stripeCharge.Id;
        }

    }
}
 