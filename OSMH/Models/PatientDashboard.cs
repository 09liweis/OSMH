using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
    public class PatientDashboard
    {
        public List<Doctor> doctors { get; set; }
        public List<Schedule> schedules { get; set; }
    }
}