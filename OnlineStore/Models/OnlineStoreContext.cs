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
                
                var customers = new CustomerDetails[]
                {
                    new CustomerDetails()
                    {
                        Fname="Dell",
                        Sname="Intel",
                        Email="dellintel@hotmail.com",
                        Telephone="012213454566",
                        BillingAddress = new List<BillingAddress>()
                        {
                            new BillingAddress()
                            {
                                City="Liverpool",
                                Country="UK",
                                HouseName="Car",
                                PostCode="L23 6TR",
                                Fullname= "Dell Intel",
                                StreetAddress= "13 Margaret Road",
                            },

                            new BillingAddress()
                            {
                                City="London",
                                Country="UK",
                                HouseName="Truck",
                                PostCode="L29 6TT",
                                Fullname= "Dell Intel",
                                StreetAddress= "19 Bank Road",
                            },
                        }
                    },

                    new CustomerDetails()
                    {
                        Fname="gttt",
                        Sname="Smith",
                        Email="dellintel@hotmail.com",
                        Telephone="012213454566",
                        BillingAddress = new List<BillingAddress>()
                        {
                            new BillingAddress()
                            {
                                City = "London",
                                Country = "UK",
                                HouseName = "Lorry",
                                PostCode = "M36 7DH",
                                Fullname = "Sam Phone",
                                StreetAddress= "4 Noodle Road",
                            },
                        }
                    }
                };

                context.Wines.AddRange(wines);
                context.CustomerDetails.AddRange(customers);
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

        public DbSet<CustomerDetails> CustomerDetails { get; set; }

        public DbSet<BillingAddress> BillingAddress { get; set; }

        public DbSet<PurchasedDetails> PurchasedDetails { get; set; }

        public DbSet<Shipping> Shipping { get; set; }

        public DbSet<Cart> Carts { get; set; }
    }
}
