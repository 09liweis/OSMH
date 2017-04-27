using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
    [Table("Patient")]
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        public string SIN { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public int User_id { get; set; }

        public virtual User User { get; set; }
        public ICollection<Appointment> appointments { get; set; }
        public ICollection<Payment> payments { get; set; }
    }
}