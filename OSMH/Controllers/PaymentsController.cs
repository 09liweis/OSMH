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
    public class PaymentsController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();

        // GET: Payments
        public ActionResult Index()
        {
            var PatientId = Convert.ToInt32(Session["patientId"]);

            List<Payment> payment = db.Payments.Where(i => i.Patient_Id == PatientId && i.Paid_In_Full == false).ToList();

            return View(payment);
        }

        // GET: ADMIN - Payments List
       public ActionResult Admin()
        {
            var payments = db.Payments.Include(p => p.Patient);
            return View(payments.ToList());
        }

    // GET: Payments/Details/5
    public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.Patient_Id = new SelectList(db.patients, "Id", "SIN");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Account_Number,Site_Number,Balance_Due,Amount_Paid,Paid_In_Full,Patient_Id")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Patient_Id = new SelectList(db.patients, "Id", "SIN", payment.Patient_Id);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Patient_Id = new SelectList(db.patients, "Id", "SIN", payment.Patient_Id);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Account_Number,Site_Number,Balance_Due,Amount_Paid,Paid_In_Full,Patient_Id")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Patient_Id = new SelectList(db.patients, "Id", "SIN", payment.Patient_Id);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmPayment(Payment payment)
        {
            if (ModelState.IsValid)
            {
                return View("Payment", new AcceptedPayment());
            }

            return View(payment);
        }
        /* follow tutorial from cmatskas.com/processing-payments-on-your-site-using-stripe-and-net/ */

        [HttpPost]
        public ActionResult Payment(AcceptedPayment model)
        {
            if (ModelState.IsValid)
            {
                var token = GetTokenId(model);
                var chargeId = ChargeCustomer(model.PaymentAmount, token);
                var payment = db.Payments.Single(d => d.Patient_Id == model.PatientId);
                payment.Amount_Paid = decimal.Parse(model.PaymentAmount.ToString());
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        private static string GetTokenId(AcceptedPayment model)
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
                Description = "OSMH material payment.",
                SourceTokenOrExistingSourceId = tokenId //check if source token is right
            };

            var chargeService = new StripeChargeService();
            var stripeCharge = chargeService.Create(myCharge);

            return stripeCharge.Id;
        }
    }
}
