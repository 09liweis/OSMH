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
        private osmhEntities db = new osmhEntities();
        // GET: Blogs
        public JsonResult Index()
        {
            return new JsonResult { Data = db.Blogs.ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}