using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
	[Table("Suggestion")]
	public class Suggestion
	{
		public enum Group
		{
			Patient, Stuff
		}
		[Key]
		public int SuggestionId { get; set; }
		[Required]
		[ForeignKey("User")]
		public int UserId { get; set; }
		[StringLength(200, MinimumLength = 5)]
		public string Title { get; set; }
		[StringLength(1000, MinimumLength = 10)]
		public string Description { get; set; }
		[Required]
		public Group GroupName { get; set; }
		public int Vote { get; set; }
		[DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
		public DateTime CreatingTime { get; set; }

		public virtual User User { get; set; }
	}
}