using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Models
{
    public class Offer
    {
        public string OfferId { get; set; }
        public decimal DiscountProcent { get; set; }
        public string CustCategoryId { get; set; }
        public virtual ICollection<PriceOffer> PriceOffers { get; set; }
        public virtual CustCategory CustCategory { get; set; }
    }
}
