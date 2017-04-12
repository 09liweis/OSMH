using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;

namespace OSMH.Controllers
{
    public class VisitorController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();
        // GET: Visitor
        public ActionResult Index()
        {
            return View();
        }
        // Post: Register visitor
        [HttpPost]
        public JsonResult regVisitor()
        {
            var result = new { Success = "true" };
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
                message.To.Add("byn9826@gmail.com");
                message.Subject = "Test";
                message.IsBodyHtml = true;
                message.Body = "test test test";
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                result = new { Success = ex.Message };
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }


        // Post: Read Admin Page
        [HttpPost]
        public JsonResult readAdmin()
        {
            VisitorLimit limitation = db.VisitorLimit.FirstOrDefault(l => l.VisitorLimit_date == DateTime.Today);
            if (limitation == null)
            {
                int id = (int)DateTime.Now.DayOfWeek;
                limitation = db.VisitorLimit.Find(id);
            }

            int regs = db.VisitorReg.Where(r => r.VisitorReg_date == DateTime.Today).Count();

            visitorAdmin visitorAdmin = new visitorAdmin();
            visitorAdmin.visitorLimit = limitation;
            visitorAdmin.visitorRegs = regs;
            return Json(visitorAdmin);
        }
        // POST: Update today's limitation
        [HttpPost]
        public JsonResult updateToday(VisitorLimit today)
        {
            DateTime timer = today.VisitorLimit_date.Value.Date;
            if (db.VisitorLimit.Any(v => v.VisitorLimit_date == timer))
            {
                VisitorLimit old = db.VisitorLimit.First(v => v.VisitorLimit_date == timer);
                old.VisitorLimit_max = today.VisitorLimit_max;
                old.VisitorLimit_start = today.VisitorLimit_start;
                old.VisitorLimit_end = today.VisitorLimit_end;
                db.VisitorLimit.Attach(old);
                db.Entry(old).State = EntityState.Modified;
            }
            else
            {
                db.VisitorLimit.Add(today);
            }
            db.SaveChanges();
            var result = new { Success = "true"};
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //POST: search a special day
        [HttpPost]
        public JsonResult searchDay(string date)
        {
            DateTime choice = Convert.ToDateTime(date);
            VisitorLimit special = db.VisitorLimit.FirstOrDefault(s => s.VisitorLimit_date == choice.Date);
            if (special == null)
            {
                int id = (int)choice.DayOfWeek;
                special = db.VisitorLimit.Find(id);
            }
            return Json(special);
        }
        //POST: read a preset day
        [HttpPost]
        public JsonResult readPreset(int date)
        {
            VisitorLimit preset = db.VisitorLimit.Find(date);
            return Json(preset);
        }
        // POST: Update day's limitation
        [HttpPost]
        public JsonResult updatePreset(VisitorLimit preset)
        {
            db.Entry(preset).State = EntityState.Modified;
            db.SaveChanges();
            var result = new { Success = "true" };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}