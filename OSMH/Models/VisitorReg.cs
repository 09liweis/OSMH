using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSMH.Models
{
    [Table("VisitorReg")]
    public class VisitorReg
    {
        [Key]
        public int VisitorReg_id { get; set; }
        public Nullable<DateTime> VisitorReg_date { get; set; }
        public string VisitorReg_code { get; set; }
        public string VisitorReg_name { get; set; }
    }
}