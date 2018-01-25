using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    //display several orders. Iterate 
    public class OrderViewModel
    {
        public IEnumerable<PurchasedDetails>ExtendedPurchaseDetails { get; set; }
    }
}
