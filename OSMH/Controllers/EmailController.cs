using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
using System.Net;
using System.Data.Entity;
using System.Net.Mail;

namespace OSMH.Controllers
{
    public class EmailController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();

        // GET: Email
        public ActionResult Index()
        {
            List<EmailPost> posts = db.EmailPost.OrderByDescending(s => s.Id).Take(5).ToList();
            return View(posts);
        }
        [HttpPost]
        public JsonResult addEmail(EmailSub sub)
        {
            if (db.EmailSub.Any(v => v.Email == sub.Email))
            {
                var fail = new { Success = "duplicate" };
                return Json(fail, JsonRequestBehavior.DenyGet);
            }
            db.EmailSub.Add(sub);
            db.SaveChanges();
            var result = new { Success = "true" };
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        // POST: Add new subscriber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EmailSub email)
        {
            if (ModelState.IsValid)
            {
                db.EmailSub.Add(email);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        // Admin: Email
        [Authorize]
        public ActionResult Admin()
        {
            List<EmailPost> posts = db.EmailPost.OrderBy(s => s.Id).ToList();
            return View(posts);
        }

        // GET: Email subscribe list
        [Authorize]
        public ActionResult Subscribe()
        {
            List<EmailSub> subs = db.EmailSub.OrderBy(s => s.Id).ToList();
            return View(subs);
        }


        // GET: Email/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //send new email
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmailPost post)
        {
            if (ModelState.IsValid)
            {
                List<EmailSub> all = db.EmailSub.OrderBy(s => s.Id).ToList();

                db.EmailPost.Add(post);
                db.SaveChanges();
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Credentials = new NetworkCredential("marvelcanada@outlook.com", "hb2017cms");
                smtpClient.EnableSsl = true;
                MailMessage message = new MailMessage();
                try
                {
                    MailAddress fromAddress = new MailAddress("marvelcanada@outlook.com");
                    smtpClient.Host = "smtp-mail.outlook.com";
                    smtpClient.Port = 587;
                    message.From = fromAddress;
                    foreach (var x in all)
                    {
                        message.To.Add(x.Email);
                    }
                    message.Subject = post.Title;
                    message.IsBodyHtml = true;
                    message.Body = post.Content;
                    smtpClient.Send(message);
                }
                catch (Exception ex)
                {
 
                }

                return RedirectToAction("Admin");
            }
            return RedirectToAction("Create");
        }

        // GET: Contact Admin Delete Page
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailPost post = db.EmailPost.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }



        // GET: Contact Admin Delete Page
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailPost post = db.EmailPost.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Delete contact section
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailPost post = db.EmailPost.Find(id);
            db.EmailPost.Remove(post);
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
    }


}