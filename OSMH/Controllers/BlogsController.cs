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
    public class BlogsController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();

        public IQueryable<DateTime> getDates()
        {
            return db.blogs.Where(b => b.Published == true).Select(b => b.PublishDate).Distinct();
        }

        public ActionResult Archive(DateTime id)
        {
            List<Blog> blogs = db.blogs.Where(b => b.PublishDate == id && b.Published == true).ToList();

            ViewBag.dates = getDates();
            return View(blogs);
        }

        // GET: Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.dates = getDates();
            return View(blog);
        }

        public ActionResult Index()
        {
            List<Blog> blogs = db.blogs.Where(b => b.Published == true).OrderByDescending(b => b.PublishDate).ToList();

            ViewBag.dates = getDates();
            return View(blogs);
        }


        //Admin site
        [Authorize(Roles = "admin")]
        public ActionResult Admin()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            return View(db.blogs.OrderByDescending(b => b.PublishDate).ToList());
        }

        // GET: Blogs/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,Published,PublishDate")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.blogs.Add(blog);
                db.SaveChanges();
                TempData["Message"] = "New blog has been created.";
                return RedirectToAction("Admin", "Blogs");
            }

            return View(blog);
        }

        // GET: Blogs/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,Published,PublishDate")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Blog " + blog.Title + " has been updated.";
                return RedirectToAction("Admin", "Blogs");
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        [Authorize(Roles ="admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.blogs.Find(id);
            db.blogs.Remove(blog);
            db.SaveChanges();
            TempData["Message"] = "Blog " + blog.Title + " has been deleted.";
            return RedirectToAction("Admin", "Blogs");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}