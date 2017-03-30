using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;

namespace OSMH.Controllers
{
    public class PatientController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();
        public ActionResult Dashboard()
        {
            if (Session["patientId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<Schedule> schedules = db.Schedules.Where(s => s.Booked == false).ToList();
            return View(schedules);
        }
    }
}