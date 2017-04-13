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
    public class MessageController : Controller
    {

        private OSMHDbContext db = new OSMHDbContext();

        // GET: Message
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message msg = db.Messages.Find(id);
            if (msg == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(msg);
            }
        }


        // GET: Testimonials/Create
        public ActionResult Admin()
        {
            return View(db.Messages.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Message")] Message msg)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(msg);
                db.SaveChanges();
                return RedirectToAction("Index", "Messages");
            }

            return View(msg);
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