using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using FoodStore.Models;
using FoodStore.ExtensionMethods;
using FoodStore.Data;

namespace FoodStore.Pages.Admin
{
    [Authorize(Roles = "administrator")]
    public class AdminModel : PageModel
    {
        public UserManager<Customer> userManager { get; set; }
        private FoodStoreContext storeContext;
  
        public AdminModel(UserManager<Customer> userManager, FoodStoreContext storeContext)
        {
            this.userManager = userManager;
            this.storeContext = storeContext;
        }

        public void OnGet()
        {
        }

        private void UpdateProductPriceOffer(CustCategory category)
        {
            var offer = storeContext.Offers.Where(p => p.CustCategoryId == category.CustCategoryId).FirstOrDefault();

            foreach(var product in storeContext.Products.ToList())
            {
                decimal newPrice = Math.Round(product.ProductPrice - (product.ProductPrice * offer.DiscountProcent), 2);
                
                storeContext.PriceOffers.Add(new PriceOffer
                {
                    PromotionalText = product.ProductName +" discounted from "+ product.ProductPrice.ToString("c"),
                    NewPrice = newPrice,
                    OfferId = offer.OfferId,
                    ProductId = product.ProductId
                });

                storeContext.SaveChanges();
            }
        }

        public async Task<JsonResult> OnGetSearch(string userName)
        {
            Customer customer = await userManager.FindByNameAsync(userName);

            return new JsonResult(customer);
        }

        public void OnPostUpdate(string userName, string claim)
        {
            var client = storeContext.Customers.Where(p => p.UserName == userName).FirstOrDefault();
            var category = storeContext.CustCategories.Where(p => p.CategoryName == claim).FirstOrDefault();

            if(category != null)
            {
                client.CustCategoryId = category.CustCategoryId;
                storeContext.SaveChanges();
                UpdateProductPriceOffer(category);
            }
            else { ModelState.AddModelError("ClaimUpdate", "This claim dosn't exist in database!"); }
        }

        public async Task OnPostDelete(string userName)
        {
            Customer customer = await userManager.FindByNameAsync(userName);
            IdentityResult result = await userManager.DeleteAsync(customer);
            result.AddIdentityErrors(ModelState);
        }

    }
}
