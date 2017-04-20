using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
	[Table("SuggestionUpvote")]
	public class SuggestionUpvote
	{
		[Key]
		public int VoteId { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		[ForeignKey("Suggestion")]
		public int SuggestionId { get; set; }

		public virtual Suggestion Suggestion { get; set; }
	}
}