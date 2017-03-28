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
            return View();
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
                return RedirectToAction("Index", "Blogs");
            }

            return View(test);
        }

        
        // GET: Testimonials/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Testimonials/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Testimonials/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Testimonials/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
