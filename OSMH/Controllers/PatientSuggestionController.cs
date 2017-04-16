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
	[Authorize(Roles = "patient")]
	public class PatientSuggestionController : ISuggestionController
    {
        // GET: PaitentSuggestion
        public override ActionResult Index(string sortOrder, string searchString)
        {
			List<UserSuggestion> userSuggestions = new List<UserSuggestion>();
			ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
			ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
			ViewBag.VoteSortParm = sortOrder == "mostPopular" ? "leastPopular" : "mostPopular";
			var suggestions = from s in db.Suggestions.Include(s => s.User).Where(s => s.GroupName == Models.Suggestion.Group.Patient)
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


			var x = suggestions.ToList();

			foreach (var suggestion in suggestions.ToList())
			{
				UserSuggestion userSuggestion = MakeAUserSuggestion(suggestion);
				userSuggestions.Add(userSuggestion);
			}

			return View(userSuggestions);
		}

	}
}