using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodStore.Data;
using FoodStore.ExtensionMethods;
using FoodStore.Models;
using FoodStore.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodStore.Pages
{
    public class CartModel : PageModel
    {
        private IProductRepo productRepo;
        public FoodStoreContext storeContext { get; }
        public IHttpContextAccessor httpContextAccessor;

        public Cart Cart { get; set; }

        public CartModel(IProductRepo productRepo, FoodStoreContext storeContext, IHttpContextAccessor httpContextAccessor)
        {
            this.productRepo = productRepo;
            this.storeContext = storeContext;
            this.httpContextAccessor = httpContextAccessor;
        }

        public void OnGet(string returnUrl)
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(string productId, string returnUrl)
        {
            var newProduct = productRepo.Products.FirstOrDefault(p => p.ProductId == productId);
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Cart.AddProdcut(newProduct, 1);
            HttpContext.Session.SetJson("cart", Cart);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
