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
			List<Message> Messages = db.Messages.ToList();
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            return View(Messages);
        }
        // GET: Message/Details
        
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


        // GET: Message/Create
        [Authorize(Roles = "admin")]
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
        public ActionResult Create([Bind(Include = "Id,Title,YourMessage")] Message msg)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(msg);
                db.SaveChanges();
                TempData["Message"] = "Your msg has been sent to admin.";
                return RedirectToAction("Index");
            }

            return View(msg);
        }


        // GET: Message/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
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
            return View(msg);
        }

        // POST: Testimonial/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,YourMessage")] Message msg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(msg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin", "Messages");
            }
            return View(msg);
        }

        // GET: Message/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
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
            return View(msg);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message msg = db.Messages.Find(id);
            db.Messages.Remove(msg);
            db.SaveChanges();
            return RedirectToAction("Admin", "Messages");
        }
    }
}