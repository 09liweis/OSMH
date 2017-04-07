using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static OSMH.Models.Suggestion;

namespace OSMH.Models.helper
{
	public class UserSuggestion
	{
		public int SuggestionId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Group GroupName { get; set; }
		public int Vote { get; set; }
		public DateTime CreatingTime { get; set; }
	}
}