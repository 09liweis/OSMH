using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
    [Table("Schedule")]
    public class Schedule
    {
        public int Id { get; set; }
        public int Doctor_id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string EndTime { get; set; }
        public bool Booked { get; set; }
    }
}