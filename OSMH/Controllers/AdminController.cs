using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
using OSMH.Models.helper;

namespace OSMH.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();
        // GET: Admin
        public ActionResult Index()
        {
			AdminIndexDataView data = GetData();
			return View(data);
        }

		[HttpPost]
		public JsonResult ReadData()
		{
			AdminIndexDataView data = GetData();
			return Json(data);
		}

		public AdminIndexDataView GetData()
		{
			AdminIndexDataView data = new AdminIndexDataView();
			data.AccountsTotal = db.users.Count();
			data.BlogTotal = db.blogs.Count();
			data.MessageTotal = db.Messages.Count();
			data.PatientSuggestions = db.Suggestions.Where(s => s.GroupName == Suggestion.Group.Patient).Count();
			data.StuffSuggestions = db.Suggestions.Where(s => s.GroupName == Suggestion.Group.Staff).Count();
			data.TestimonialsTotal = db.Testimonials.Count();
			data.VistorsTotal = db.VisitorReg.Where(r => r.VisitorReg_date == DateTime.Today).Count();
			data.JobTotal = db.Jobs.Count();
			data.EmailSubscribers = db.EmailSub.Count();
			return data;
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