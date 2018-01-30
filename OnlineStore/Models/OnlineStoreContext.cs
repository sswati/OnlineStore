using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public class OnlineStoreContext : DbContext
    {
        public class OnlineStoreInitializer : DropCreateDatabaseAlways<OnlineStoreContext>
        {
            protected override void Seed(OnlineStoreContext context)
            {
                var products = new Product[]
                {
                    new Product()
                    {
                        Description = "Sweet",
                        //District="France",
                        //Type=Type.White,
                        Price = 10.00M,
                        Name = "Chardonnay",
                        Properties = new List<PropertyAndValue>()
                        {
                            new PropertyAndValue()
                            {
                                Property = new Property()
                                {
                                    Name = "Wine Type"
                                },

                                Value = "White"
                            }
                        }
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

        public OnlineStoreContext() : base("DefaultConnection")
        {
            this.Database.CommandTimeout = 180;
            Database.SetInitializer<OnlineStoreContext>(new OnlineStoreInitializer());
        }

        //public DbSet<Wine> Wines { get; set; }

        //public DbSet<Chocolate> Chocolates { get; set; }

        public DbSet<BillingAddress> BillingAddress { get; set; }

        public DbSet<PurchasedDetails> PurchasedDetails { get; set; }

        public DbSet<Shipping> Shipping { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<PropertyAndValue> ProductProperties { get; set; }

        public DbSet<Property> ProductPropertyNames { get; set; }
    }
}
