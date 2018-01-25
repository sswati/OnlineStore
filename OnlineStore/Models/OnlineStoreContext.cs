using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public class OnlineStoreContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public class OnlineStoreInitializer : DropCreateDatabaseAlways<OnlineStoreContext>
        {
            protected override void Seed(OnlineStoreContext context)
            {
                var billingAddress = new BillingAddress[]
                {
                    new Models.BillingAddress()
                    {
                        CustomerDetailsId=1,
                        BillingAddressId=1,
                        City="Liverpool",
                        Country="UK",
                        HouseName="Car",
                        PostCode="L23 6TR",
                        Fullname= "Dell Intel",
                        StreetAddress= "13 Margaret Road",
                    },

                    new Models.BillingAddress()
                    {
                        CustomerDetailsId=1,
                        BillingAddressId=2,
                        City="London",
                        Country="UK",
                        HouseName="Truck",
                        PostCode="L29 6TT",
                        Fullname= "Dell Intel",
                        StreetAddress= "19 Bank Road",
                    },

                    new Models.BillingAddress()
                    {
                        CustomerDetailsId=2,
                        BillingAddressId=3,
                        City="London",
                        Country="UK",
                        HouseName="Lorry",
                        PostCode="M36 7DH",
                        Fullname= "Sam Phone",
                        StreetAddress= "4 Noodle Road",
                    },
                };

                var wines = new Wine[]
                {
                    new Wine()
                    {
                        WineId=1,
                        Description="Sweet",
                        District="France",
                        Type=Type.White,
                        Price=10.00M,
                        Name="Chardonnay",
                        chocolates = new Chocolate[]
                        {
                            new Chocolate()
                            {
                                ChocolateId=1,
                                CocoaContent= CocoaContent.Dark,
                                Name="Brazil Nuts",
                                Price=10.00M,
                            },

                            new Chocolate()
                            {
                                ChocolateId=2,
                                CocoaContent= CocoaContent.Milk,
                                Name="Mango",
                                Price=12.00M,
                            }
                        }.AsEnumerable<Chocolate>()
                    }
                };

                var customers = new CustomerDetails[]
                {
                    new Models.CustomerDetails()
                    {
                        CustomerId=1,
                        Fname="Dell",
                        Sname="Intel",
                        Email="dellintel@hotmail.com",
                        Telephone="012213454566",
                    },

                    new Models.CustomerDetails()
                    {
                        CustomerId =2,
                        Fname="gttt",
                        Sname="Smith",
                        Email="dellintel@hotmail.com",
                        Telephone="012213454566",
                    }
                };

                context.Wines.AddRange(wines);
                context.CustomerDetails.AddRange(customers);
                context.BillingAddress.AddRange(billingAddress);
                context.SaveChanges();
            }
        }

        public OnlineStoreContext() : base("name=OnlineStoreContext")
        {
            Database.SetInitializer<OnlineStoreContext>(new OnlineStoreInitializer());

        }

        public DbSet<Wine> Wines { get; set; }

        public DbSet<Chocolate> Chocolates { get; set; }

        public DbSet<CustomerDetails> CustomerDetails { get; set; }

        public DbSet<BillingAddress> BillingAddress { get; set; }

        public DbSet<PurchasedDetails> PurchasedDetails { get; set; }

        public DbSet<Shipping> Shipping { get; set; }
    }
}

