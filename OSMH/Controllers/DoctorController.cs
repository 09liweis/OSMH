using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
namespace OSMH.Controllers
{
    public class DoctorController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();
        // GET: Doctor
        public ActionResult Dashboard()
        {
            if (Session["doctorId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult CreateSchedule()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSchedule(Schedule schedule)
        {
            if (Session["doctorId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var doctorId = Session["doctorId"];
            schedule.Doctor_id = Convert.ToInt32(doctorId);
            DateTime startTime = DateTime.Parse(schedule.StartTime);
            DateTime endTime = DateTime.Parse(schedule.EndTime);
            db.Schedules.Add(schedule);
            db.SaveChanges();

            return RedirectToAction("Dashboard");
        }
    }
}