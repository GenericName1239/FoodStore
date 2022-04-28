using FoodStore.Data;
using FoodStore.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Models.EFModels
{
    public class EFProductModel : IProductRepo
    {
        private FoodStoreContext foodStoreContext;

        public EFProductModel(FoodStoreContext foodStoreContext)
        {
            this.foodStoreContext = foodStoreContext;
        }

        public IQueryable<Product> Products => foodStoreContext.Products;
    }
}
