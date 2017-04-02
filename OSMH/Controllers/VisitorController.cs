using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
using System.Data.Entity;

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
        // Post: Read Public Page
        // GET: Visitor Admin Page
        public ActionResult Admin()
        {
            return View();
        }
        // Post: Read Admin Page
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
    }
}