using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace OSMH.Models
{
    [Table("EmailCategory")]
    public class EmailCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string category { get; set; }
    }
}