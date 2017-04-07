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
    public class SuggestionController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();

        // GET: Suggestion
        public ActionResult Index(string sortOrder, string searchString)
        {
			List<UserSuggestion> userSuggestions = new List<UserSuggestion>();
			ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
			ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
			ViewBag.VoteSortParm = sortOrder == "mostPopular" ? "leastPopular" : "mostPopular";
			ViewBag.GroupSortParm = sortOrder == "patient" ? "stuff" : "patient";
			var suggestions = from s in db.Suggestions.Include("User")
							  select s;

			if (!String.IsNullOrEmpty(searchString))
			{
				suggestions = suggestions.Where(s => s.Title.Contains(searchString)
											|| s.Description.Contains(searchString)
											|| s.CreatingTime.ToString().Contains(searchString));
			}

			switch (sortOrder)
			{
				case "title_desc":
					suggestions = suggestions.OrderByDescending(s => s.Title);
					break;
				case "Date":
					suggestions = suggestions.OrderBy(s => s.CreatingTime);
					break;
				case "date_desc":
					suggestions = suggestions.OrderByDescending(s => s.CreatingTime);
					break;
				case "mostPopular":
					suggestions = suggestions.OrderByDescending(s => s.Vote);
					break;
				case "leastPopular":
					suggestions = suggestions.OrderBy(s => s.Vote);
					break;
				case "stuff":
					suggestions = suggestions.OrderBy(s => s.GroupName == Suggestion.Group.Patient);
					break;
				case "patient":
					suggestions = suggestions.OrderBy(s => s.GroupName == Suggestion.Group.Stuff);
					break;
				default:
					suggestions = suggestions.OrderBy(s => s.Title);
					break;
			}


			foreach (var suggestion in suggestions.ToList())
			{
				UserSuggestion userSuggestion = new UserSuggestion();
				userSuggestion.SuggestionId = suggestion.SuggestionId;
				userSuggestion.FirstName = suggestion.User.FirstName;
				userSuggestion.LastName = suggestion.User.LastName;
				userSuggestion.Title = suggestion.Title;
				userSuggestion.Description = suggestion.Description;
				userSuggestion.CreatingTime = suggestion.CreatingTime;
				userSuggestion.Vote = suggestion.Vote;
				userSuggestion.UserName = suggestion.User.UserName;
				userSuggestion.GroupName = suggestion.GroupName;
				userSuggestions.Add(userSuggestion);
			}

            return View(userSuggestions);
        }

		// GET: Suggestion/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suggestion suggestion = db.Suggestions.Find(id);
            if (suggestion == null)
            {
                return HttpNotFound();
            }
            return View(suggestion);
        }

        // GET: Suggestion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suggestion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SuggestionId,UserId,Title,Description,GroupName,Vote,CreatingTime")] Suggestion suggestion)
        {
            if (ModelState.IsValid)
            {
                db.Suggestions.Add(suggestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(suggestion);
        }

        // GET: Suggestion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suggestion suggestion = db.Suggestions.Find(id);
            if (suggestion == null)
            {
                return HttpNotFound();
            }
            return View(suggestion);
        }

        // POST: Suggestion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SuggestionId,UserId,Title,Description,GroupName,Vote,CreatingTime")] Suggestion suggestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suggestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suggestion);
        }

        // GET: Suggestion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suggestion suggestion = db.Suggestions.Find(id);
            if (suggestion == null)
            {
                return HttpNotFound();
            }
            return View(suggestion);
        }

        // POST: Suggestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Suggestion suggestion = db.Suggestions.Find(id);
            db.Suggestions.Remove(suggestion);
            db.SaveChanges();
            return RedirectToAction("Index");
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
