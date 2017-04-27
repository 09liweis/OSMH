using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
    [Table("Donations")]
    public partial class Donations
    {

        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name ="Department")]
        public int? Department_Id { get; set; }

        [Display(Name = "Full Name")]
        public string Full_Name  { get; set; }

        [Required(ErrorMessage = "Total Amount is required")]
        [Display(Name = "Total Amount")]
        public decimal Total_Amount { get; set; }

        public bool Anonymity { get; set; }

        [Display(Name = "Department")]
        [ForeignKey("Department_Id")]
        public virtual Department Department { get; set; }

    }
}