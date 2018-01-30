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

    public class Chocolate : Product
    {
        [Required(ErrorMessage = "Please Select the Cocoa percentage ")]
        [DisplayName("Cocoa Content")]
        public CocoaContent CocoaContent { get; set; }
    }
}

//customerdetails (1):(M)PurchasedDetails