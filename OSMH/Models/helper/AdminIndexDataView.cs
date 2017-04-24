using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSMH.Models.helper
{
	public class AdminIndexDataView
	{
		public int? PatientSuggestions { get; set; }
		public int? StuffSuggestions { get; set; }
		public int? VistorsTotal { get; set; }
		public int? BlogTotal { get; set; }
		public int? AccountsTotal { get; set; }
		public int? MessageTotal { get; set; }
		public int? TestimonialsTotal { get; set; }
		public int? JobTotal { get; set; }
		public int? EmailSubscribers { get; set; }
	}
}