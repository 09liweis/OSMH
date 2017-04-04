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
		{	[Display(Name = "Fire investigation")]
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
		public string Title { get; set; }
		public AlertClass Classification { get; set; }
		public string Message { get; set; }
		public Status AlertStatus { get; set; }
		public string Publisher { get; set; }
		[Display(Name = "Created at")]
		public DateTime? CreatingTime { get; set; }
		[Display(Name = "Published at")]
		public DateTime? PublishingTime { get; set; }
	}
}