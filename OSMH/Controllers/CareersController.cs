using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
using System.IO;

namespace OSMH.Controllers
{
    public class CareersController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();


        //GET: Jobs - User
        public ActionResult Index()
        {
            return View(db.Jobs.ToList());
        }

        //GET: Applicant - Create
        public ActionResult ApplyNow(int? id)
        {

            if(id == null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }


        //POST: Submit Application - Create Applicant
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplyNow([Bind(Include = "Id,Full_Name,Applied_Date,Email,Resume,Action_Completed,Job_Id")] Applicant applicant, HttpPostedFileBase file, int id)
        {
        
            if (ModelState.IsValid)
            {
                if( file.ContentLength>0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Resumes"), fileName);
                    applicant.Resume = fileName;
                    file.SaveAs(path);
                }
                applicant.Job_Id = id;
                applicant.Applied_Date = DateTime.Now;
                db.Applicants.Add(applicant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicant);
        }

        //GET: Appplicant based On Job Id : Admin
        public ActionResult Applicants(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Admin");
            }


            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return RedirectToAction("Admin");
            }

            return View(job);
        }    

        public ActionResult Download(string name)
        {
            string fileName = name;

            return File("~/App_Data/Resumes/"+fileName, "content-dispostion", fileName);
           
        }

        // GET: Jobs: Admin 
        [Authorize]
        public ActionResult Admin()
        {
 
            
            return View(db.Jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return RedirectToAction("Index");
            }
            return View(job);
        }



        // GET: Jobs/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Department_Id = new SelectList(db.Departments, "Id", "Name");
            ViewBag.JobType_Id = new SelectList(db.JobTypes, "Id", "Name");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Job_Title,Closing_Date,Job_Type,Department_Id,JobType_Id")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }

            return View(job);
        }

        // GET: Jobs/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Admin");
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return RedirectToAction("Admin");
            }
            ViewBag.Department_Id = new SelectList(db.Departments, "Id", "Name");
            ViewBag.JobType_Id = new SelectList(db.JobTypes, "Id", "Name");

            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Job_Title,Closing_Date,Department_Id,JobType_Id")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(job);
        }

        // GET: Jobs/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Admin");
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return RedirectToAction("Admin");
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Admin");
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
