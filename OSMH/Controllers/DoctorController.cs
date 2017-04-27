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
        [Authorize(Roles = "doctor")]
        public ActionResult Admin()
        {
            if (!Auth.checkLogin())
            {
                return RedirectToAction("Login", "Account");
            }
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            return View();
        }

        [Authorize(Roles = "doctor")]
        public ActionResult Edit()
        {
            int doctorId = Convert.ToInt32(Session["doctorId"]);
            return View(db.doctors.Find(doctorId));
        }

        [HttpPost]
        [Authorize(Roles = "doctor")]
        public ActionResult Edit(Doctor doctor)
        {
            db.Entry(doctor.User).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Message"] = "Profile has been updated.";
            return RedirectToAction("Admin");
        }

        [Authorize(Roles = "doctor")]
        public ActionResult CreateSchedule()
        {
            if (Session["doctorId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "doctor")]
        public ActionResult CreateSchedule(Schedule schedule)
        {
            TimeSpan startTime = schedule.StartTime;
            TimeSpan endTime = schedule.EndTime;

            int numSchedule = 0;
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
                numSchedule++;
            }
            TempData["Message"] =  numSchedule + " Available Schedules have been created for " + schedule.Date.ToString("yyyy-MM-dd");
            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize(Roles = "doctor")] 
        public JsonResult cancelSchedule(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
            db.SaveChanges();
            return this.getSchedules();
        }

        public JsonResult getAppointments()
        {
            int doctorId = Convert.ToInt32(Session["doctorId"]);
            var appointments = db.Appointments.Where(a => a.schedule.Doctor_id == doctorId).Select(a => new { a.Id, a.patient.User.FirstName, a.patient.User.LastName, a.schedule.Date, a.schedule.StartTime, a.schedule.EndTime }).ToList();
            return new JsonResult { Data = appointments, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult getSchedules()
        {
            int doctorId = Convert.ToInt32(Session["doctorId"]);
            var schedules = db.Schedules.Where(s => s.Doctor_id == doctorId && s.Date >= DateTime.Today).Select(s => new { s.Id, s.Date, s.StartTime, s.EndTime, s.Booked }).OrderByDescending(s => s.Date).ToList();
            return new JsonResult { Data = schedules, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        //get a list of doctors with availabe future schedules
        public JsonResult List()
        {
            var doctors = db.Schedules.Where(s => s.Date >= DateTime.Today).Select(s => new { s.Doctor.Id, s.Doctor.User.FirstName, s.Doctor.User.LastName }).Distinct().ToList();
            return new JsonResult { Data = doctors, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}