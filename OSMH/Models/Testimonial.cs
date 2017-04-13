using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSMH.Models
{
    [Table("Testimonial")]
    public class Testimonial
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }

}