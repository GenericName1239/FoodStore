using FoodStore.Data;
using FoodStore.Infrastructure;
using FoodStore.Models.Interfaces;
using FoodStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IProductRepo productRepo;
        private FoodStoreContext storeContext;
        private Trie trie;
        private ProductDict productDict;
        private int pageSize = 4;

        public HomeController(IProductRepo productRepo, FoodStoreContext storeContext, Trie trie, ProductDict productDict)
        {
            this.productRepo = productRepo;
            this.storeContext = storeContext;
            this.trie = trie;
            this.productDict = productDict;
        }

        public IActionResult HomeView(string category, int pageIndx = 1)
        {
            var discountProducts = storeContext.Customers.Where(p => p.UserName == User.Identity.Name)
                                 .Join(storeContext.CustCategories, user => user.CustCategoryId,
                                 custCat => custCat.CustCategoryId, (user, custCat) => new { user, custCat })
                                 .Join(storeContext.Offers, _custCat => _custCat.custCat.CustCategoryId,
                                 offer => offer.CustCategoryId, (_custCat, offer) => new { _custCat, offer })
                                 .Join(storeContext.PriceOffers, _offer => _offer.offer.OfferId,
                                 prodOffer => prodOffer.OfferId, (_offer, prodOffer) => new { _offer, prodOffer });

            var products = productRepo.Products.OrderBy(p => p.ProductId)
                        .Where(p => category == null || p.Category.CategoryName == category)
                        .Skip((pageIndx - 1) * pageSize)
                        .Take(pageSize);

            foreach(var product in products)
            {
                var discount = discountProducts.Where(p => p.prodOffer.ProductId == product.ProductId).FirstOrDefault();
                if(discount != null) { product.ProductPrice = discount.prodOffer.NewPrice; }
            }

            var ViewModel = new ProductListViewModel
            {
                Products = products,
                PageInfo = new PageInfo
                {
                    CurrentPage = pageIndx,
                    TotalProducts = category == null ? productRepo.Products.Count() :
                            productRepo.Products.Where(p => p.Category.CategoryName == category).Count(),
                    PageSize = pageSize
                },
                CurrentCategory = category
            };

            if(productDict.productMap.Count() == 0)
            {
                foreach (var product in productRepo.Products)
                {
                    productDict.productMap.Add(product.ProductName.ToLower(), product);
                    trie.Insert(product.ProductName.ToLower());
                }
            }
            

            return View(ViewModel);
        }
    }
}
