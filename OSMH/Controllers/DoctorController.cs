using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
using OSMH.Models.helper;
using System.Data.Entity;

namespace OSMH.Controllers
{
    public class DoctorController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();
        public ActionResult Admin()
        {
            //Temporary test
            //Session["doctorId"] = "1";
            if (!Auth.checkLogin())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
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

            while (startTime < endTime)
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

        public JsonResult getAppointments()
        {
            int doctorId = Convert.ToInt32(Session["doctorId"]);
            var appointments = db.Appointments.Where(a => a.schedule.Doctor_id == doctorId).Select(a => new { a.Id, a.patient.user.FirstName, a.patient.user.LastName, a.schedule.Date, a.schedule.StartTime, a.schedule.EndTime }).ToList();
            return new JsonResult { Data = appointments, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult getSchedules()
        {
            int doctorId = Convert.ToInt32(Session["doctorId"]);
            var schedules = db.Schedules.Where(s => s.Doctor_id == doctorId).Select(s => new { s.Id, s.Date, s.StartTime, s.EndTime, s.Booked }).ToList();
            return new JsonResult { Data = schedules, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        //
        public JsonResult List()
        {
            var doctors = db.doctors.Select(d => new { d.Id, d.User.FirstName, d.User.LastName }).ToList();
            return new JsonResult { Data = doctors, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}