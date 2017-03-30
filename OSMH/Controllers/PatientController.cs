using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
using System.Data.Entity;

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

            ViewBag.doctors = db.users.Where(u => u.Role == "doctor").ToList();
            List<Schedule> schedules = db.Schedules.Where(s => s.Booked == false).ToList();
            PatientDashboard pd = new PatientDashboard()
            {
                doctors = db.doctors.ToList(),
                schedules = schedules
            };
            return View(pd);
        }

        public JsonResult BookAppointment(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            schedule.Booked = true;
            db.Entry(schedule).State = EntityState.Modified;
            db.SaveChanges();
            Appointment appointment = new Appointment()
            {
                Schedule_Id = id,
                Patient_Id = Convert.ToInt32(Session["patientId"]),
            };
            db.Appointments.Add(appointment);
            db.SaveChanges();
            return new JsonResult { Data = "success" };
        }
    }
}