using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace OSMH.Models
{
    [Table("EmailPost")]
    public class EmailPost
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [AllowHtml]
        public string Content { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Publish Date")]
        public DateTime date { get; set; }
        [Required]
        public string category { get; set; }
    }
}