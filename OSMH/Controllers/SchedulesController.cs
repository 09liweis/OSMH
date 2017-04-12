using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSMH.Models;
using System.Data.Entity;

namespace OSMH.Controllers
{
    public class SchedulesController : Controller
    {
        private OSMHDbContext db = new OSMHDbContext();
        // GET: Schedules
        public JsonResult Doctor(int id)
        {
            List<DateTime> dates = db.Schedules.Where(s => s.Doctor_id == id).Select(s => s.Date).Distinct().ToList();
            return new JsonResult { Data = dates, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult TimeSlot(DateTime id)
        {
            int doctorId = Convert.ToInt32(Request.QueryString["doctorId"]);
            var schedules = db.Schedules
                .Where(s => s.Date == id && s.Doctor_id == doctorId && s.Booked == false)
                .Select(s => new { s.Id, s.StartTime, s.EndTime })
                .OrderBy(s => s.StartTime)
                .ToList();
            return new JsonResult { Data = schedules, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}