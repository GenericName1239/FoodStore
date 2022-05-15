using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Models
{
    public class PriceOffer
    {
        public int PriceOfferId { get; set; }
        public string PromotionalText { get; set; }
        public decimal NewPrice { get; set; }
        public string OfferId { get; set; }
        public string ProductId { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual Product Product { get; set; }
    }
}
