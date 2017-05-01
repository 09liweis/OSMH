using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSMH.Models
{

    [Table("Message")]
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string YourMessage { get; set; }
    }
}