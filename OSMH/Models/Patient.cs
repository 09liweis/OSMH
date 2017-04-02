using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
    [Table("Patient")]
    public class Patient
    {
        public int Id { get; set; }
        public string SIN { get; set; }
        public DateTime DOB { get; set; }
        public int User_id { get; set; }

        public virtual User user { get; set; }
        public ICollection<Appointment> appointments { get; set; }
    }
}