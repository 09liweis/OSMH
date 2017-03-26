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
    public class ContactController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();
        // GET: Contact Us Public Page
        public ActionResult Index()
        {
            List<ContactUs> contacts = db.ContactUs.Where(c => c.ContactUs_public == true)
                                                   .OrderBy(s => s.ContactUs_id)
                                                   .ToList();
            return View(contacts);
        }
        // GET: Contact Us Admin Page
        public ActionResult Admin()
        {
            List<ContactUs> contacts = db.ContactUs.OrderBy(s => s.ContactUs_id)
                                                   .ToList();
            return View(contacts);
        }
        // GET: Contact Us Admin Create Page
        public ActionResult Create()
        {
            return View();
        }
        // POST: Create New contact page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactUs contact)
        {
            if (ModelState.IsValid)
            {
                db.ContactUs.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return RedirectToAction("Create");
        }
        // GET: edit contact page
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUs contact = db.ContactUs.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }
        // POST: Update contact info
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactUs contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(contact);
        }
        // GET: Contact Admin Delete Page
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUs contact = db.ContactUs.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }
        // POST: Delete contact section
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactUs contact = db.ContactUs.Find(id);
            db.ContactUs.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Admin");
        }
    }
}