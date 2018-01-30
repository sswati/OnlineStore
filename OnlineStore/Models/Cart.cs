using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public string CustomerId { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
        public Shipping Shipping { get; set; }

        private decimal _TotalPrice { get; set; }

        [NotMapped]
        public decimal TotalPrice
        {
            get
            {
                this._TotalPrice = CartItems.Select(p => p.Quantity * p.Product.Price).Sum();

                if (this._TotalPrice != 0)
                {
                    this._TotalPrice += Shipping.Price;
                }

                return this._TotalPrice;
            }

            set
            {
                this._TotalPrice = value;
            }
        }
    }
}