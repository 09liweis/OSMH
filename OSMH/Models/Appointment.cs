using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
    [Table("Appointment")]
    public class Appointment
    {
        public int Id { get; set; }
        public int Schedule_Id { get; set; }
        public int Patient_Id { get; set; }

        public virtual Schedule schedule { get; set; }
        public virtual Patient patient { get; set; }
    }
}