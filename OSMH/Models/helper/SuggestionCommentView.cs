using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSMH.Models.helper
{
	public class SuggestionCommentView
	{
		public int CommentId { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }
		public int SuggestionId { get; set; }
		public string Description { get; set; }
		public DateTime CreatingTime { get; set; }
	}
}