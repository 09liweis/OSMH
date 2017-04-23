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

        public ActionResult Members()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            return View(db.users.ToList());
        }

        [HttpGet]
        public ActionResult CreateMember()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMember(User user)
        {
            db.users.Add(user);
            db.SaveChanges();

            if (user.Role == "doctor")
            {
                Doctor doctor = new Doctor()
                {
                    User = user
                };
                db.doctors.Add(doctor);
                db.SaveChanges();
            }
            TempData["Message"] = "New member has been created.";
            return RedirectToAction("Members");
        }
    }
}