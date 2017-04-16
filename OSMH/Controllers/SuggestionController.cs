using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
using OSMH.Models.helper;

namespace OSMH.Controllers
{
	[Authorize(Roles = "admin")]
	public class SuggestionController : ISuggestionController
    {
		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
