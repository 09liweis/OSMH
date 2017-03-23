using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumberWebDevProject.Models
{
	public class Alert
	{
		public enum Status
		{
			Inactive, Ongoing, Archived
		}

		public enum AlertClass
		{
			Fire, PoliceInvestigation, HazardousWeather, Unknown
		}

		public int AlertID { get; set; }
		public string Title { get; set; }
		public AlertClass Classification { get; set; }
		public string Message { get; set; }
		public Status AlertStatus { get; set; }
		public string Publisher { get; set; }
		public DateTime CreatingTime { get; set; }
		public DateTime PublishingTime { get; set; }

		public virtual AlertArchive AlertArchive { get; set; }
	}
}