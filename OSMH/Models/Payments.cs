using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
    [Table("Payments")]
    public partial class Payment
    {

        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(7)]
        [Display(Name ="Account Number")]
        public string Account_Number {get;set;} 

        [Required(ErrorMessage = "Required field")]
        [StringLength(7)]
        [Display(Name = "Site Number")]
        public int Site_Number { get; set; }

        [Display(Name = "Balance Due")]
        public decimal Balance_Due { get; set; }

        [Display(Name = "Amount Paid")]
        public decimal Amount_Paid { get; set; }
        
        [Display(Name = "Paid In Full")]
        public bool Paid_In_Full { get; set; }

        [Display(Name = "Patient Name")]
        public int Patient_Id { get; set; }

        [ForeignKey("Patient_Id")]
        public virtual Patient Patient { get; set; }
  

    }
}