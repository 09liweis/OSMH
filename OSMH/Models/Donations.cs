using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
    [Table("Jobs")]
    public partial class Job
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Job()
        {
            this.Applicants = new HashSet<Applicant>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Job Title is required")]
        [Display(Name = "Job Title")]
        public string Job_Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Closing Date")]
        public DateTime Closing_Date { get; set; }

        [Required(ErrorMessage ="Department is required")]
        [Display(Name = "Department")]
        public int Department_Id { get; set; }

        [Required(ErrorMessage = "Job Type is required")]
        [Display(Name = "Job Type")]
        public int JobType_Id { get; set; }



        [ForeignKey("Department_Id")]
        public virtual Department Department { get; set; }
   
        [ForeignKey("JobType_Id")]
        public virtual JobType JobType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Applicant> Applicants { get; set; }
    }
}