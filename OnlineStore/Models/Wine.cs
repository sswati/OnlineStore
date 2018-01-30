using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public enum Type
    {
        Red,
        White,
    }
    public class Wine : Product
    {
        public Type Type { get; set; }

        [Required(ErrorMessage = "Enter the district of the Wine")]
        [StringLength(100, MinimumLength = 3)]
        public string District { get; set; }

        public virtual ICollection<Chocolate> Chocolates { get; set; }
    }
}