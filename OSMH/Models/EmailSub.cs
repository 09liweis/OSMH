using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace OSMH.Models
{
    [Table("EmailSub")]
    public class EmailSub
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Contact Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}