using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class AddressViewModel
    {
        public IEnumerable<System.Web.Mvc.SelectListItem> CountriesList { get; set; }

        public BillingAddress BillingAddress { get; set; }
    }
}

        
