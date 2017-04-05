using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OSMH.Models;

namespace OSMH.Models.helper
{
	public class AlertJson
	{
		public string Title { get; set; }
		public string Message { get; set; }
		public string Status { get; set; }
		public string PublishingTime { get; set; }
		public Boolean Active { get; set; }
	}
}