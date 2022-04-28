using FoodStore.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepo productRepo;

        public NavigationMenuViewComponent(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(productRepo.Products.Select(p => p.Category)
                .Distinct()
                .OrderBy(c => c.CategoryName));
        }
    }
}
