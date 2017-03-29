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
                limitation = db.VisitorLimit.Find(1);
            }

            int regs = db.VisitorReg.Where(r => r.VisitorReg_date == DateTime.Today).Count();

            visitorAdmin visitorAdmin = new visitorAdmin();
            visitorAdmin.visitorLimit = limitation;
            visitorAdmin.visitorRegs = regs;

            return Json(visitorAdmin);
        }
    }
}