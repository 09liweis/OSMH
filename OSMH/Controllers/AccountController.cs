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
        public ActionResult Register(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                db.userAccounts.Add(userAccount);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = userAccount.UserName + " Succesfully registered";
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount userAccount)
        {
            var user = db.userAccounts.Where(u => u.Email == userAccount.Email && u.Password == userAccount.Password).FirstOrDefault();
            if (user != null)
            {
                Session["userId"] = user.Id.ToString();
                Session["userName"] = user.UserName.ToString();
                if (user.Role == "doctor")
                {
                    Doctor doctor = db.doctors.Where(d => d.User_id == user.Id).FirstOrDefault();
                    Session["doctorId"] = doctor.Id.ToString();
                    Session["name"] = doctor.FirstName.ToString() + " " + doctor.LastName.ToString();
                    return RedirectToAction("Dashboard", "Doctor");
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