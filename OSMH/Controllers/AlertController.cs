using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
using OSMH.Models.helper;

namespace OSMH.Controllers
{
    public class AlertController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();

		// GET: Alert
		[Authorize(Roles = "admin")]
		public ActionResult Index()
        {
			List<Alert> alerts = db.Alerts.Where(m => m.AlertStatus != Alert.Status.Archived).ToList();

			return View(alerts);
        }

		// GET Archive
		[Authorize(Roles = "admin")]
		public ActionResult Archive()
		{
			List<Alert> alerts = db.Alerts.Where(m => m.AlertStatus == Alert.Status.Archived).ToList();

			return View(alerts);
		}

		// GET: Alert/Details/5
		[Authorize(Roles = "admin")]
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }

		// GET: Alert/Create
		[Authorize(Roles = "admin")]
		public ActionResult Create()
        {
            return View();
        }

		// POST: Alert/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlertID,Title,Classification,Message,AlertStatus, Publisher, CreatingTime")] Alert alert)
        {

			if (ModelState.IsValid)
            {
                db.Alerts.Add(alert);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }

            return View(alert);
        }

		// GET: Alert/Edit/5
		[Authorize(Roles = "admin")]
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }

		// POST: Alert/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlertID,Title,Classification,Message,AlertStatus,Publisher,CreatingTime,PublishingTime")] Alert alert)
        {
            if (ModelState.IsValid)
            {
				db.Entry(alert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(alert);
        }

		[Authorize(Roles = "admin")]
		public ActionResult SwitchActive(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			int activeAlert = db.Alerts.Where(m => m.AlertStatus == Alert.Status.Ongoing).Count();

			if (activeAlert >= 1)
			{
				ViewBag.ActiveAlertError = "Only one alert can be active.";
				List<Alert> allAlerts = db.Alerts.Where(m => m.AlertStatus != Alert.Status.Archived).ToList();
				return View("Admin", allAlerts);
			}

			Alert alert = db.Alerts.Find(id);

			alert.AlertStatus = Alert.Status.Ongoing;
			alert.PublishingTime = DateTime.Now;
			
			db.Entry(alert).State = EntityState.Modified;
			db.SaveChanges();

			if (alert == null)
			{
				return HttpNotFound();
			}
			List<Alert> alerts = db.Alerts.Where(m => m.AlertStatus != Alert.Status.Archived).ToList();
			return View("Admin", alerts);
		}

		[Authorize(Roles = "admin")]
		public ActionResult SwitchInactive(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Alert alert = db.Alerts.Find(id);

			alert.AlertStatus = Alert.Status.Inactive;

			db.Entry(alert).State = EntityState.Modified;
			db.SaveChanges();

			if (alert == null)
			{
				return HttpNotFound();
			}
			List<Alert> alerts = db.Alerts.Where(m => m.AlertStatus != Alert.Status.Archived).ToList();
			return View("Admin", alerts);
		}

		// GET: Alert/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult AddtoArchive(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }

        // POST: Alert/Delete/5
        [HttpPost, ActionName("AddtoArchive")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult AddtoArchive(int id)
        {
            Alert alert = db.Alerts.Find(id);
			alert.AlertStatus = Alert.Status.Archived;
			db.Entry(alert).State = EntityState.Modified;
			db.SaveChanges();
			return RedirectToAction("Archive");
        }

		public JsonResult GetActive()
		{
			AlertJson alertView = new AlertJson();
			var alertsQuery = from a in db.Alerts
							  where a.AlertStatus == Alert.Status.Ongoing
							  select a;
			List<Alert> alersList = alertsQuery.ToList();

			if (alersList.Count == 1 )
			{
				Alert alert = alersList[0];
				alertView.Active = true;
				alertView.Title = alert.Title;
				alertView.Message = alert.Message;
				alertView.Status = "Ongoing";
				if (alert.PublishingTime.HasValue)
					alertView.PublishingTime = alert.PublishingTime.Value.ToString();
				else
					alertView.PublishingTime = DateTime.Now.ToString();
			}
			else
			{
				alertView.Active = false;
			} 

			return Json(alertView, JsonRequestBehavior.AllowGet);
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
