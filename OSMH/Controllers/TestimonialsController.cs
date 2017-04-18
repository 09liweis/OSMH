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
    public class TestimonialsController : Controller
    {

        private OSMHDbContext db = new OSMHDbContext();

        // GET: Testimonials
        public ActionResult Index()
        {
            List<Testimonial> Testimonials = db.Testimonials.ToList();
            
            return View(Testimonials);
        }

        // GET: Testimonials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial test = db.Testimonials.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(test);
            }
        }


        // GET: Testimonials/Create
        public ActionResult Admin()
        {
            return View(db.Testimonials.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Fname,Lname,Email,Contact,Title,Message")] Testimonial test)
        {
            if (ModelState.IsValid)
            {
                db.Testimonials.Add(test);
                db.SaveChanges();
                return RedirectToAction("Index", "Testimonials");
            }

            return View(test);
        }

    
        // POST: Testimonial/Edit/5
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial test = db.Testimonials.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // POST: Testimonial/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,FName,LName,Email,Contact,Title,Message")] Testimonial test)
        {
            if (ModelState.IsValid)
            {
                db.Entry(test).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin", "Testimonials");
            }
            return View(test);
        }


        // GET: Testimonial/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial test = db.Testimonials.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // POST: Testimonial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Testimonial test = db.Testimonials.Find(id);
            db.Testimonials.Remove(test);
            db.SaveChanges();
            return RedirectToAction("Admin", "Testimonials");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
