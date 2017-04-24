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
                return RedirectToAction("Index");
            }

            return View(msg);
        }


        // GET: Message/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Message/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Admin");
            }
            catch
            {
                return View();
            }
        }

        // GET: Message/Delete/5
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
            return RedirectToAction("Admin");
        }
    }
}