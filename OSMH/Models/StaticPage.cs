using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSMH.Models
{
	[Table("StaticPage")]
	public class StaticPage
	{
		public enum Menu
		{
			[Display(Name = "Service: Cure")]
			Service_cure,
			[Display(Name = "Service: Care")]
			Service_care,
			Patients,
			Visitors,
			Resources,
			Additional,
			Legal,
			About
		}
		public enum StaticPageStatus
		{
			unpublished, published
		}
		public int Id { get; set; }
		[Required]
		[StringLength(200, MinimumLength = 5)]
		public string Title { get; set; }
		[Required]
		[Display(Name = "Menu Title")]
		public Menu MenuTitle { get; set; }
		[Required]
		[AllowHtml]
		[StringLength(int.MaxValue, MinimumLength = 7)]
		public string Content { get; set; }
		public StaticPageStatus PageStatus { get; set; }
		[DataType(DataType.Date)]
		[Display(Name = "Publish Date")]
		public DateTime? PublishDate { get; set; }
		[Required]
		[DataType(DataType.Date)]
		[Display(Name = "Create Date")]
		public DateTime CreateDate { get; set; }
		[Required]
		public int UserId { get; set; }

		public virtual User User { get; set; }
	}
}