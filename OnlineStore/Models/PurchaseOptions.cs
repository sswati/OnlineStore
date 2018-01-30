using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public enum CardType
    {
        Visa,
        AmericanExpress,
        MasterCard,
        Maestro
    }
    public class PaymentOptions
    {
        [Key]
        public int PaymentOptionId { get; set; }
        public string Name { get; set; }
        public CardType CardTypes { get; set; }
        public DateTime ExpDate { get; set; }
        public int CardNo { get; set; }
        public int CVV { get; set; }
        public string CustomerId { get; set; }
    }
}