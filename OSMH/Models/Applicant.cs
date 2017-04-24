using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
    [Table("Applicant")]
    public partial class Applicant
    {
        public Applicant()
        {
            Action_Completed = "No Action";
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Full Name")]
        public string Full_Name { get; set; }
        [Display(Name = "Date Applied")]
        [DataType(DataType.DateTime)]
        public DateTime? Applied_Date { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string Resume { get; set; }
        public string Action_Completed { get; set; }
        public int Job_Id { get; set; }

        [ForeignKey("Job_Id")]
        public virtual Job Jobs { get; set; }


    }
}