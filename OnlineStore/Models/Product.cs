using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a valid name.")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter a valid description.")]
        [StringLength(100, MinimumLength = 3)]
        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<PropertyAndValue> Properties { get; set; }
    }
}