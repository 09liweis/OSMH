using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
namespace SomeeTest.Controllers
{
    public class AccountController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = user.UserName + " Succesfully registered";
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            var user = db.users.Where(ur => ur.Email == u.Email && ur.Password == u.Password).FirstOrDefault();
            if (user != null)
            {
                Session["userId"] = user.Id.ToString();
                Session["name"] = user.FirstName.ToString() + " " + user.LastName.ToString();
                if (user.Role == "patient")
                {
                    Patient patient = db.patients.Where(p => p.User_id == user.Id).FirstOrDefault();
                    Session["patientId"] = patient.Id.ToString();
                    return RedirectToAction("Dashboard", "Patient");
                }
                if (user.Role == "doctor")
                {
                    Doctor doctor = db.doctors.Where(d => d.User_id == user.Id).FirstOrDefault();
                    Session["doctorId"] = doctor.Id.ToString();
                    return RedirectToAction("Admin", "Doctor");
                }
                return RedirectToAction("Loggedin");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is not correct");
            }

            return View();
        }

        public ActionResult Loggedin()
        {
            if (Session["userId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            if (Session["userId"] != null)
            {
                Session.Abandon();
            }
            return RedirectToAction("Login");

        }
    }
}