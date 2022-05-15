using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Models.ViewModels
{
    public class ProductCartViewModel
    {
        public CartLine CartLine { get; set; }
        public string PromotionalText { get; set; }
        public decimal DiscountPercent { get; set; }
    }
}
