﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public class Shipping
    {
        [Key]
        public int ShippingId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Details { get; set; }

        //1 cart has 1 shipping option 
    }
}