using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;

namespace OSMH.Controllers
{
    public class HomeController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();
        // GET: Home
        public ActionResult Index()
        {
            Blog blog = db.blogs.Where(b => b.Published == true).OrderByDescending(b => b.PublishDate).First();
            Testimonial test = db.Testimonials.Where(t => t.Approval == true).First();
            Homepage homepage = new Homepage()
            {
                blog = blog,
                testimonial = test
            };
            return View(homepage);
        }
    }
}