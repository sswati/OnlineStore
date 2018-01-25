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
    public class Wine
    {
        [Key]
        public int WineId { get; set; }

        public Type Type { get; set; }


        [Required(ErrorMessage = "Enter the name of a Wine")]
        
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

       [Required(ErrorMessage = "Enter the district of the Wine")]
        [StringLength(100, MinimumLength = 3)]
        public string District { get; set; }

        [Required(ErrorMessage = "Enter a descritpion of the Wine")]
        [StringLength(100, MinimumLength = 3)]
        public string Description { get; set; }
        
        public string Image { get; set; }

        
        public decimal Price { get; set; }

        public IEnumerable<Chocolate> chocolates { get; set; }

        public int ChocolateId { get; set; }
        
        
    }
}