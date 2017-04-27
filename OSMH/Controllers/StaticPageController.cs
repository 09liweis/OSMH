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
    public class StaticPageController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();

		// GET: StaticPage
		[Authorize(Roles = "admin")]
		public ActionResult Index(string sortOrder, string SearchString)
        {
			if (TempData["Message"] != null)
			{
				ViewBag.Message = TempData["Message"];
			}

			List<StaticPage> staticpages = new List<StaticPage>();
			var allStaticpages = from s in db.StaticPages
								 select s;

			if (!String.IsNullOrEmpty(SearchString))
			{
				allStaticpages = allStaticpages.Where(s => s.Title.Contains(SearchString));
			}

			switch (sortOrder)
			{
				case "Additional":
					staticpages = allStaticpages.Where(s => s.MenuTitle == StaticPage.Menu.Additional).OrderBy(s => s.Title).ToList();
					break;
				case "Legal":
					staticpages = allStaticpages.Where(s => s.MenuTitle == StaticPage.Menu.Legal).OrderBy(s => s.Title).ToList();
					break;
				case "Service_Cure":
					staticpages = allStaticpages.Where(s => s.MenuTitle == StaticPage.Menu.Service_cure).OrderBy(s => s.Title).ToList();
					break;
				case "Service_Care":
					staticpages = allStaticpages.Where(s => s.MenuTitle == StaticPage.Menu.Service_care).OrderBy(s => s.Title).ToList();
					break;
				case "Patients":
					staticpages = allStaticpages.Where(s => s.MenuTitle == StaticPage.Menu.Patients).OrderBy(s => s.Title).ToList();
					break;
				case "Visitors":
					staticpages = allStaticpages.Where(s => s.MenuTitle == StaticPage.Menu.Visitors).OrderBy(s => s.Title).ToList();
					break;
				case "Resources":
					staticpages = allStaticpages.Where(s => s.MenuTitle == StaticPage.Menu.Resources).OrderBy(s => s.Title).ToList();
					break;
				case "About":
					staticpages = allStaticpages.Where(s => s.MenuTitle == StaticPage.Menu.About).OrderBy(s => s.Title).ToList();
					break;
				default:
					staticpages = allStaticpages.OrderBy(s => s.Title).ToList();
					break;
			}

			return View(staticpages);
        }

		public ActionResult TogglePublish(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			StaticPage staticPage = db.StaticPages.Find(id);
			if (staticPage == null)
			{
				return HttpNotFound();
			}
			if (staticPage.PageStatus == StaticPage.StaticPageStatus.published)
			{
				staticPage.PageStatus = StaticPage.StaticPageStatus.unpublished;
			}
			else if (staticPage.PageStatus == StaticPage.StaticPageStatus.unpublished)
			{
				staticPage.PageStatus = StaticPage.StaticPageStatus.published;
			}
			if (ModelState.IsValid)
			{
				db.Entry(staticPage).State = EntityState.Modified;
				db.SaveChanges();
			}
			return RedirectToAction("Index");
		}

        // GET: StaticPage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaticPage staticPage = db.StaticPages.Find(id);
            if (staticPage == null)
            {
                return HttpNotFound();
            }
            return View(staticPage);
        }

		// GET: StaticPage/Create
		[Authorize(Roles = "admin")]
		public ActionResult Create()
        {
            return View();
        }

		// POST: StaticPage/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,MenuTitle,Content")] StaticPage staticPage)
        {
			staticPage.CreateDate = DateTime.Now;
			staticPage.UserId = db.users.Where(u => u.Email == User.Identity.Name).Single().Id;
			staticPage.PageStatus = StaticPage.StaticPageStatus.unpublished;
			if (ModelState.IsValid)
            {
                db.StaticPages.Add(staticPage);
                db.SaveChanges();
				TempData["Message"] = "New static page has been created.";
				return RedirectToAction("Index");
            }

            return View(staticPage);
        }

		// GET: StaticPage/Edit/5
		[Authorize(Roles = "admin")]
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaticPage staticPage = db.StaticPages.Find(id);
            if (staticPage == null)
            {
                return HttpNotFound();
            }
            return View(staticPage);
        }

		// POST: StaticPage/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,MenuTitle,Content,CreateDate,UserId")] StaticPage staticPage)
        {
			if (ModelState.IsValid)
            {
                db.Entry(staticPage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staticPage);
        }

		// GET: StaticPage/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaticPage staticPage = db.StaticPages.Find(id);
            if (staticPage == null)
            {
                return HttpNotFound();
            }
            return View(staticPage);
        }

		// POST: StaticPage/Delete/5
		[Authorize(Roles = "admin")]
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaticPage staticPage = db.StaticPages.Find(id);
            db.StaticPages.Remove(staticPage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		[HttpPost]
		public JsonResult GetLinks()
		{
			MenuLinksGroup allStaticPagesLinks = new MenuLinksGroup();

			var allStaticPages = from s in db.StaticPages.Where(s => s.PageStatus == StaticPage.StaticPageStatus.published)
								 select s;

			List<StaticPage> additionals = allStaticPages.Where(s => s.MenuTitle == StaticPage.Menu.Additional).OrderBy(s => s.Title).ToList();
			List<StaticPage> legals = allStaticPages.Where(s => s.MenuTitle == StaticPage.Menu.Legal).OrderBy(s => s.Title).ToList();
			List<StaticPage> serviceCares = allStaticPages.Where(s => s.MenuTitle == StaticPage.Menu.Service_care).OrderBy(s => s.Title).ToList();
			List<StaticPage> serviceCures = allStaticPages.Where(s => s.MenuTitle == StaticPage.Menu.Service_cure).OrderBy(s => s.Title).ToList();
			List<StaticPage> patients = allStaticPages.Where(s => s.MenuTitle == StaticPage.Menu.Patients).OrderBy(s => s.Title).ToList();
			List<StaticPage> visitors = allStaticPages.Where(s => s.MenuTitle == StaticPage.Menu.Visitors).OrderBy(s => s.Title).ToList();
			List<StaticPage> resources = allStaticPages.Where(s => s.MenuTitle == StaticPage.Menu.Resources).OrderBy(s => s.Title).ToList();
			List<StaticPage> abouts = allStaticPages.Where(s => s.MenuTitle == StaticPage.Menu.About).OrderBy(s => s.Title).ToList();

			allStaticPagesLinks.AdditionalLinks = PutLinkToList(additionals);
			allStaticPagesLinks.LegalLinks = PutLinkToList(legals);
			allStaticPagesLinks.PatientsLinks = PutLinkToList(patients);
			allStaticPagesLinks.ServiceCareLinks = PutLinkToList(serviceCares);
			allStaticPagesLinks.ServiceCureLinks = PutLinkToList(serviceCures);
			allStaticPagesLinks.VisitorsLinks = PutLinkToList(visitors);
			allStaticPagesLinks.ResourcesLinks = PutLinkToList(resources);
			allStaticPagesLinks.AboutLinks = PutLinkToList(abouts);

			return Json(allStaticPagesLinks);
		}

		public List<MenuLink> PutLinkToList(List<StaticPage> pages)
		{
			List<MenuLink> pageLinks = new List<MenuLink>();
			foreach (var page in pages)
			{
				MenuLink link = new MenuLink();
				link.PageId = page.Id;
				link.PageTitle = page.Title;
				pageLinks.Add(link);
			}
			return pageLinks;
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
