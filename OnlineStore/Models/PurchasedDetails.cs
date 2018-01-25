using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{



    public class PurchasedDetails
    {
        [Key]
        public int PurchasedDetailsId { get; set; }



        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime Date { get; set; }

        [Range(1.00, 100.00,
            ErrorMessage = "Price must be between 1.00 and 100.00")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }


        public Shipping Shipping { get; set; }
        public virtual IEnumerable<Chocolate> Chocolate { get; set; }

        public virtual IEnumerable<Wine> Wine { get; set; }

        public virtual CustomerDetails CustomerDetails { get; set; }


        public int WineQuantity { get; set; }

        public int ChocoQuantity { get; set; }

        [NotMapped]
        private Dictionary<Chocolate, int> ChocolateList { get; set; }

        [NotMapped]
        private Dictionary<Wine, int> WineList { get; set; }


        private decimal? _TotalPrice;

        [NotMapped]
        public decimal? TotalPrice

        {

            get
            {

                this.TotalPrice = ChocolateList.Select(p => p.Value * p.Key.Price).Sum();

                this.TotalPrice += WineList.Select(p => p.Value * p.Key.Price).Sum();

                this.TotalPrice += Shipping.Price;

                return this._TotalPrice;
            }

            set
            {

                this._TotalPrice = value;


            }

        }
    }
}


