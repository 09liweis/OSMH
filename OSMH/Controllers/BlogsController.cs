using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OSMH.Models;
namespace OSMH.Controllers
{
    public class BlogsController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();
        // GET: Blogs
        public ActionResult Index()
        {
            List<Blog> blogs = db.blogs.ToList();
            return View(blogs);
        }
    }
}