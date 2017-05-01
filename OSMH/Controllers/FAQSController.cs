using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OSMH.Models;
using System.Net;
using System.Data.Entity;

namespace OSMH.Controllers
{
    public class FAQSController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();
        // GET: FAQ
        public ActionResult Index()
        {
            List<FAQ> FAQs = db.FAQs.ToList();
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            return View(FAQs);
        }
        // GET: FAQ/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQ f = db.FAQs.Find(id);
            if (f == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(f);
            }
        }


        // GET: FAQ/Create
        [Authorize(Roles = "admin")]
        public ActionResult Admin()
        {

            return View(db.FAQs.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ques,Ans,Date")] FAQ f)
        {
            if (ModelState.IsValid)
            {
               
                db.FAQs.Add(f);
                db.SaveChanges();
                TempData["Message"] = "Your question/answer has been sent to admin.";
                return RedirectToAction("Index", "FAQs");
            }

            return View(f);
        }


        // POST: FAQ/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQ f = db.FAQs.Find(id);
            if (f == null)
            {
                return HttpNotFound();
            }
            return View(f);
        }

        // POST: FAQ/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ques,Ans,Date")] FAQ f)
        {
            if (ModelState.IsValid)
            {
                db.Entry(f).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin", "FAQs");
            }
            return View(f);
        }

        //FAQTestimonial/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQ f = db.FAQs.Find(id);
            if (f == null)
            {
                return HttpNotFound();
            }
            return View(f);
        }

        // POST: FAQ/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FAQ f = db.FAQs.Find(id);
            db.FAQs.Remove(f);
            db.SaveChanges();
            return RedirectToAction("Admin", "FAQs");
        }


        
    }
}