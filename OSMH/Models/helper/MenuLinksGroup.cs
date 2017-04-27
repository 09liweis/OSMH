using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSMH.Models.helper
{
	public class MenuLinksGroup
	{
		public List<MenuLink> AdditionalLinks { get; set; }
		public List<MenuLink> LegalLinks { get; set; }
		public List<MenuLink> ServiceCareLinks { get; set; }
		public List<MenuLink> ServiceCureLinks { get; set; }
		public List<MenuLink> PatientsLinks { get; set; }
		public List<MenuLink> VisitorsLinks { get; set; }
		public List<MenuLink> ResourcesLinks { get; set; }
		public List<MenuLink> AboutLinks { get; set; }

		public MenuLinksGroup()
		{
			this.AdditionalLinks = new List<MenuLink>();
			this.LegalLinks = new List<MenuLink>();
			this.ServiceCareLinks = new List<MenuLink>();
			this.ServiceCureLinks = new List<MenuLink>();
			this.PatientsLinks = new List<MenuLink>();
			this.VisitorsLinks = new List<MenuLink>();
			this.ResourcesLinks = new List<MenuLink>();
			this.AboutLinks = new List<MenuLink>();
		}
	}
}