using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSMH.Models
{
    [Table("FAQ")]
    public class FAQ
    {
        public int Id { get; set; }
        [Required]
        public string Ques { get; set; }
        [Required]
        public string Ans { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}