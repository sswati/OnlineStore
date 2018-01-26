using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public string CustomerId { get; set; }
        public virtual ICollection<Wine> Wine { get; set; }

        public virtual ICollection<Chocolate> Chocolate {get;set;}
    }
}