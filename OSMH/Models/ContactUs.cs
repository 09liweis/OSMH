using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
    [Table("ContactUs")]
    public class ContactUs
    {
        [Key]
        public int ContactUs_id { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Name length must between 2 - 16", MinimumLength = 2)]
        [Display(Name = "Contact Name")]
        public string ContactUs_name { get; set; }
        [Display(Name = "Contact Info")]
        public string ContactUs_info { get; set; }
        [Display(Name = "Contact Email")]
        [DataType(DataType.EmailAddress)]
        public string ContactUs_email { get; set; }
        [Display(Name = "Contact Phone")]
        public string ContactUs_phone { get; set; }
        [Display(Name = "Show in public or not")]
        public bool ContactUs_public { get; set; }
    }
}