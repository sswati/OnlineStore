using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public class CustomerDetails
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "A First Name is required")]
        [DisplayName("First Name")]
        [StringLength(100, MinimumLength =3)]
        public string Fname { get; set; }

        [Required(ErrorMessage = "A Surname is required")]
        [DisplayName("Surname")]
        [StringLength(100, MinimumLength =2)]
        public string Sname { get; set; }

        [Required(ErrorMessage = "A contact number is required")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "An email address is required")]
        public string Email { get; set; }

      public IEnumerable<BillingAddress> BillingAddress { get; set; }


        public IEnumerable<PurchasedDetails> PurchasedDetails { get; set; }
    }
}


