using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;

namespace OSMH.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Doctors()
        {
            return View();
        }

        public JsonResult CreateDoctor(User user)
        {
            user.Role = "doctor";
            db.users.Add(user);
            db.SaveChanges();

            Doctor doctor = new Doctor()
            {
                User = user
            };
            db.doctors.Add(doctor);
            db.SaveChanges();

            return new JsonResult { };
        }
    }
}