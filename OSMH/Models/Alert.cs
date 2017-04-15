using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
	[Table("Alert")]
	public class Alert
	{
		public enum Status
		{
			Inactive, Ongoing, Archived
		}

		public enum AlertClass
		{
			[Display(Name = "Fire investigation")]
			FireInvestigation,
			[Display(Name = "Police investigation")]
			PoliceInvestigation,
			[Display(Name = "Hazardous weather")]
			HazardousWeather,
			[Display(Name = "Public health issue")]
			Publichealth,
			[Display(Name = "Ongoing situation")]
			Unknown
		}

		public int AlertID { get; set; }
		[StringLength(20, MinimumLength = 5)]
		public string Title { get; set; }
		[Required]
		public AlertClass Classification { get; set; }
		[StringLength(200, MinimumLength = 10)]
		public string Message { get; set; }
		[Required]
		[Display(Name = "Alert Status")]
		public Status AlertStatus { get; set; }
		[Required]
		public string Publisher { get; set; }
		[Display(Name = "Created at")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
		[Required]
		public DateTime? CreatingTime { get; set; }
		[Display(Name = "Published at")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
		[DataType(DataType.DateTime)]
		public DateTime? PublishingTime { get; set; }
	}
}