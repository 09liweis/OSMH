using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static OSMH.Models.Suggestion;

namespace OSMH.Models.helper
{
	public class UserSuggestion
	{
		public int UserId { get; set; }
		public int SuggestionId { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName
		{
			get
			{
				return LastName + ", " + FirstName;
			}
			set
			{
				UserName = value;
			}
		}
		public string Title { get; set; }
		public string Description { get; set; }
		public Group GroupName { get; set; }
		public int Vote { get; set; }
		public DateTime CreatingTime { get; set; }
		public int NumComments { get; set; }
		public string SystemMessage { get; set; }

		public List<SuggestionCommentView> SuggestionCommentViews { get; set; }
		public List<SuggestionUpvote> SuggestionUpvotes { get; set; }

		public UserSuggestion()
		{
			this.SuggestionCommentViews = new List<SuggestionCommentView>();
			this.SuggestionUpvotes = new List<SuggestionUpvote>();
		}
	}
}