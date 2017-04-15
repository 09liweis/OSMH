using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
using System.Net;
using System.Data.Entity;

namespace OSMH.Controllers
{
    public class EmailController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        // GET: Email/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}