using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumberWebDevProject.Models
{
	public class AlertArchive
	{
		[ForeignKey("Alert")]
		public int AlertArchiveID { get; set; }
		public DateTime ArchivedTime { get; set; }
		// Admin Name should be AdminID once intergraded with other feature
		public string AdminName { get; set; }

		public virtual Alert Alert { get; set; }
	}
}