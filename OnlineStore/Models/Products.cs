using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{       

    public class Products
    {
        
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public IDictionary<string, string> Chocolate { get; set; }

        public IDictionary<string, string> Wines { get; set; }

    }
}