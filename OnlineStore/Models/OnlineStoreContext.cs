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
                var wines = new Wine[]
                {
                    new Wine()
                    {
                        Description="Sweet",
                        District="France",
                        Type=Type.White,
                        Price=10.00M,
                        Name="Chardonnay",
                        Chocolates = new List<Chocolate>()
                        {
                            new Chocolate()
                            {
                                CocoaContent = CocoaContent.Dark,
                                Name = "Brazil Nuts",
                                Price=10.00M,
                            },
                            new Chocolate()
                            {
                                CocoaContent=CocoaContent.Milk,
                                Name="Mango",
                                Price=12.00M,
                            }
                        }
                    }
                };
                
                context.Wines.AddRange(wines);
                context.SaveChanges();
            }
        }

        public OnlineStoreContext() : base("DefaultConnection")
        {
            this.Database.CommandTimeout = 180;
            Database.SetInitializer<OnlineStoreContext>(new OnlineStoreInitializer());
        }

        public DbSet<Wine> Wines { get; set; }

        public DbSet<Chocolate> Chocolates { get; set; }

        public DbSet<BillingAddress> BillingAddress { get; set; }

        public DbSet<PurchasedDetails> PurchasedDetails { get; set; }

        public DbSet<Shipping> Shipping { get; set; }

        public DbSet<Cart> Carts { get; set; }
    }
}
