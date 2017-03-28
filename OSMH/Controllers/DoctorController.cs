using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
using OSMH.Models.helper;
namespace OSMH.Controllers
{
    public class DoctorController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();
        // GET: Doctor
        public ActionResult Admin()
        {
            //Temporary test
            Session["doctorId"] = "1";
            if (!Auth.checkLogin())
            {
                return RedirectToAction("Login", "Account");
            }

            List<Schedule> schedules = db.Schedules.ToList();
            return View(schedules);
        }

        public ActionResult CreateSchedule()
        {
            if (Session["doctorId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateSchedule(Schedule schedule)
        {
            TimeSpan startTime = schedule.StartTime;
            TimeSpan endTime = schedule.EndTime;

            db.Schedules.Add(schedule);
            db.SaveChanges();

            while (startTime > endTime)
            {
                TimeSpan nextEndTime = startTime.Add(TimeSpan.FromMinutes(30));
                Schedule newSchedule = new Schedule();
                newSchedule.Doctor_id = schedule.Doctor_id;
                newSchedule.StartTime = startTime;
                newSchedule.EndTime = nextEndTime;
                newSchedule.Date = schedule.Date;
                db.Schedules.Add(newSchedule);
                db.SaveChanges();

                startTime = nextEndTime;
            }

            return RedirectToAction("Admin");
        }
    }
}