using System;
using System.Collections.Generic;
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
            this.Jobs = new HashSet<Job>();
        }

        public int Id { get; set; }
        public DateTime Applied_Date { get; set; }
        public string Email { get; set; }
        public string Resume { get; set; }
        public int? Action_Completed { get; set; }
        public int User_Id { get; set; }

        [ForeignKey("User_Id")]
        public virtual User UserAccount { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }

    }
}