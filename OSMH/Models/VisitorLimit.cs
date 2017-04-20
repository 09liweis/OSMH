using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSMH.Models
{
    [Table("VisitorLimit")]
    public class VisitorLimit
    {
        [Key]
        public int VisitorLimit_id { get; set; }
        [Required]
        public int VisitorLimit_max { get; set; }
        [Required]
        public int VisitorLimit_start { get; set; }
        [Required]
        public int VisitorLimit_end { get; set; }
        public Nullable<DateTime> VisitorLimit_date { get; set; }
    }
}