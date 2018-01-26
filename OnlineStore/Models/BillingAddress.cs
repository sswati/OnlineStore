using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public class BillingAddress
    {
        [Key]
        public int BillingAddressId { get; set; }

        public string CustomerId { get; set; }
        public string Fullname { get; set; }

        [DisplayName("House Name")]
        [StringLength(100, MinimumLength = 2)]
        public string HouseName { get; set; }

        [Required(ErrorMessage = "The 1st line address is required")]
        [DisplayName("Address")]
        [StringLength(300, MinimumLength = 5)]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "The City is required")]
        [StringLength(60, MinimumLength = 3)]
        public string City { get; set; }

        [Required(ErrorMessage = "The Post Code is required")]
        [DisplayName("Post Code")]
        [StringLength(15, MinimumLength = 3)]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "The Country is required")]
        [StringLength(50, MinimumLength = 2)]
        public string Country { get; set; }
    }
}