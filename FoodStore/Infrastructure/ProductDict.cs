using FoodStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Infrastructure
{
    public class ProductDict
    {
        public Dictionary<string, Product> productMap { get; set; }

        public ProductDict()
        {
            productMap = new Dictionary<string, Product>();
        }
    }
}
