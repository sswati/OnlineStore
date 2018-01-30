using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public enum ProductType
    {
        Chocolate,
        Wine
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        
        public Product Product { get; set; }
    }
}