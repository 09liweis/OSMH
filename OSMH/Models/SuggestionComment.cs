using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
	[Table("SuggestionComment")]
	public class SuggestionComment
	{
		[Key]
		public int CommentId { get; set; }
		[Required]
		[ForeignKey("User")]
		public int UserId { get; set; }
		[Required]
		[ForeignKey("Suggestion")]
		public int SuggestionId { get; set; }
		[Required]
		[StringLength(1000, MinimumLength = 10)]
		public string Description { get; set; }
		[Required]
		[DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
		public DateTime CreatingTime { get; set; }

		public virtual User User { get; set; }
		public virtual Suggestion Suggestion { get; set; }
	}
}