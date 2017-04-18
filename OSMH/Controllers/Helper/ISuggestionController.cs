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
	public class ISuggestionController : Controller
	{
		protected OSMHDbContext db = new OSMHDbContext();

		// GET: Suggestion
		public virtual ActionResult Index(string sortOrder, string searchString)
		{
			List<UserSuggestion> userSuggestions = new List<UserSuggestion>();
			ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
			ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
			ViewBag.VoteSortParm = sortOrder == "mostPopular" ? "leastPopular" : "mostPopular";
			ViewBag.GroupSortParm = sortOrder == "patient" ? "stuff" : "patient";
			var suggestions = from s in db.Suggestions.Include(s => s.User)
							  select s;
			//var suggestions = db.Suggestions.Include(s => s.User);

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
					suggestions = suggestions.OrderBy(s => s.GroupName == Suggestion.Group.Staff);
					break;
				default:
					suggestions = suggestions.OrderBy(s => s.Title);
					break;
			}


			foreach (var suggestion in suggestions.ToList())
			{
				UserSuggestion userSuggestion = MakeAUserSuggestion(suggestion);
				userSuggestions.Add(userSuggestion);
			}

			return View(userSuggestions);
		}

		public ActionResult MySuggestion(string sortOrder, string searchString)
		{
			int currentUserId = db.users.Where(u => u.Email == User.Identity.Name).Single().Id;
			List<UserSuggestion> userSuggestions = new List<UserSuggestion>();
			ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
			ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
			ViewBag.VoteSortParm = sortOrder == "mostPopular" ? "leastPopular" : "mostPopular";
			var suggestions = from s in db.Suggestions.Include("User").Where(s => s.UserId == currentUserId)
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
				default:
					suggestions = suggestions.OrderBy(s => s.Title);
					break;
			}


			foreach (var suggestion in suggestions.ToList())
			{
				UserSuggestion userSuggestion = MakeAUserSuggestion(suggestion);
				userSuggestions.Add(userSuggestion);
			}

			return View(userSuggestions);
		}

		// GET: Suggestion/Details/5
		public virtual ActionResult Details(int? id, string message)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Suggestion suggestion = db.Suggestions.Find(id);
			UserSuggestion userSuggestion = MakeAUserSuggestion(suggestion, "detail");

			if (!String.IsNullOrWhiteSpace(message))
			{
				userSuggestion.SystemMessage = message;
			}

			if (suggestion == null)
			{
				return HttpNotFound();
			}
			return View(userSuggestion);
		}

		public ActionResult Create()
		{
			return View();
		}

		// POST: Suggestion/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "UserId, Title, Description, GroupName")] Suggestion suggestion)
		{
			suggestion.CreatingTime = DateTime.Now;
			suggestion.UserId = db.users.Where(u => u.Email == User.Identity.Name).Single().Id;

			if (ModelState.IsValid)
			{
				db.Suggestions.Add(suggestion);
				db.SaveChanges();
				return RedirectToAction("Admin");
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
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "UserId, Title, Description, GroupName, Vote, CreatingTime, SuggestionId")] Suggestion suggestion)
		{
			if (ModelState.IsValid)
			{
				db.Entry(suggestion).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Admin");
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
			return RedirectToAction("Admin");
		}

		protected UserSuggestion MakeAUserSuggestion(Suggestion suggestion, string flag = "index")
		{
			UserSuggestion userSuggestion = new UserSuggestion();
			userSuggestion.UserId = suggestion.UserId;
			userSuggestion.SuggestionId = suggestion.SuggestionId;
			userSuggestion.FirstName = suggestion.User.FirstName;
			userSuggestion.LastName = suggestion.User.LastName;
			userSuggestion.Title = suggestion.Title;
			userSuggestion.Description = suggestion.Description;
			userSuggestion.CreatingTime = suggestion.CreatingTime;
			userSuggestion.Vote = suggestion.Vote;
			userSuggestion.GroupName = suggestion.GroupName;
			userSuggestion.NumComments = suggestion.SuggestionComments.Count;
			userSuggestion.Email = suggestion.User.Email;

			foreach (var upvote in suggestion.SuggestionUpvotes)
			{
				userSuggestion.SuggestionUpvotes.Add(upvote);
			}

			if (flag == "detail")
			{
				foreach (var comment in suggestion.SuggestionComments)
				{
					SuggestionCommentView suggestionCommentView = new SuggestionCommentView();
					suggestionCommentView.CommentId = comment.CommentId;
					suggestionCommentView.UserId = comment.UserId;
					suggestionCommentView.UserName = comment.User.LastName + ", " + comment.User.FirstName;
					suggestionCommentView.Email = comment.User.Email;
					suggestionCommentView.SuggestionId = comment.SuggestionId;
					suggestionCommentView.Description = comment.Description;
					suggestionCommentView.CreatingTime = comment.CreatingTime;
					userSuggestion.SuggestionCommentViews.Add(suggestionCommentView);
				}
			}

			return userSuggestion;
		}

		[HttpPost]
		public JsonResult Upvote(int? id)
		{
			SuggestionUpvoteJsonReturnView json = new SuggestionUpvoteJsonReturnView();
			SuggestionUpvote upvote = new SuggestionUpvote();
			if (id == null)
			{
				json.Success = false;
				return Json(json);
			}
			Suggestion suggestion = db.Suggestions.Find(id);
			if (suggestion == null)
			{
				json.Success = false;
				return Json(json);
			}

			upvote.SuggestionId = suggestion.SuggestionId;
			upvote.FirstName = User.Identity.Name;
			db.SuggestionUpvotes.Add(upvote);
			suggestion.Vote = suggestion.SuggestionUpvotes.Count;
			db.SaveChanges();
			json.Success = true;
			json.Upvotes = suggestion.Vote;

			return Json(json);
		}

		[HttpPost]
		public JsonResult DownVote(int? id, string userName)
		{
			SuggestionUpvoteJsonReturnView json = new SuggestionUpvoteJsonReturnView();
			if (id == null)
			{
				json.Success = false;
				return Json(json);
			}
			Suggestion suggestion = db.Suggestions.Find(id);
			if (suggestion == null)
			{
				json.Success = false;
				return Json(json);
			}

			SuggestionUpvote upvote = db.SuggestionUpvotes.Where(s => s.SuggestionId == id).Where(s => s.FirstName == userName).Single();
			db.SuggestionUpvotes.Remove(upvote);
			suggestion.Vote = suggestion.SuggestionUpvotes.Count;
			db.SaveChanges();
			json.Upvotes = db.SuggestionUpvotes.Where(s => s.SuggestionId == suggestion.SuggestionId).Count();
			json.Success = true;

			return Json(json);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateComment([Bind(Include = "SuggestionId, Description")] SuggestionComment comment)
		{
			comment.CreatingTime = DateTime.Now;
			comment.UserId = db.users.Where(u => u.Email == User.Identity.Name).Single().Id; ;

			if (ModelState.IsValid)
			{
				db.SuggestionComments.Add(comment);
				db.SaveChanges();
				return RedirectToAction("Details", new { id = comment.SuggestionId });
			}

			return RedirectToAction("Details", new { id = comment.SuggestionId, message = "Your comment is not submitted successfully" });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditComment([Bind(Include = "SuggestionId, Description, CreatingTime, UserId, CommentId")] SuggestionCommentView comment)
		{
			SuggestionComment suggestionComment = db.SuggestionComments.Find(comment.CommentId);
			suggestionComment.Description = comment.Description;
			if (ModelState.IsValid)
			{
				db.Entry(suggestionComment).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Details", new { id = comment.SuggestionId });
			}
			return RedirectToAction("Details", new { id = comment.SuggestionId, message = "Your comment is not editted successfully" });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteComment(int id)
		{
			SuggestionComment comment = db.SuggestionComments.Find(id);
			int suggestionId = comment.SuggestionId;
			db.SuggestionComments.Remove(comment);
			db.SaveChanges();
			return RedirectToAction("Details", new { id = suggestionId });
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
