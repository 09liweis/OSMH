using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int User_id { get; set; }
    }
}