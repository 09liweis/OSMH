using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
    public partial class AcceptedPayment
    {
        [Key]
        [Display(Name = "Payment Amount")]
        [DataType(DataType.Currency)]
        public decimal PaymentAmount { get; set; }

        [Display(Name = "CVC Code")]
        [DataType(DataType.Text)]
        public string CardCvc { get; set; }

        [Display(Name = "Expiration Month")]
        [Range(1, 12)]
        public int CardExpirationMonth { get; set; }

        [Display(Name = "Expiration Year")]
        [Range(2017, 2060)]
        public int CardExpirationYear { get; set; }

        [Display(Name = "Full Name on Card")]
        [DataType(DataType.Text)]
        public string CardName { get; set; }

        [Display(Name = "Credit Card Number")]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }

        [Display(Name = "Enter Currecy[ex. CAD]")]
        [DataType(DataType.Text)]
        public string Currency { get; set; }

        public int PatientId { get; set; }

    }
}