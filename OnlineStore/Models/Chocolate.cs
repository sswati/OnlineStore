using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public enum CocoaContent
    {
        Dark=70,
        MediumDark=60,
        SmoothDark=54,
        Milk=46,
    }

    public class Chocolate
    {
        [Key]
        public int ChocolateId { get; set; }

        [Required(ErrorMessage = "Please Select the Cocoa percentage ")]
        public CocoaContent CocoaContent { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image { get; set; }
        public decimal Price { get; set; }
        //public IEnumerable<Wine> Wines { get; set; }
    }
}

//customerdetails (1):(M)PurchasedDetails