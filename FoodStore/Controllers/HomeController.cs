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
    [Authorize(Roles = "administrator")]
    public class HomeController : Controller
    {
        private IProductRepo productRepo;
        private Trie trie;
        private ProductDict productDict;
        private int pageSize = 4;

        public HomeController(IProductRepo productRepo, Trie trie, ProductDict productDict)
        {
            this.productRepo = productRepo;
            this.trie = trie;
            this.productDict = productDict;
        }

        public IActionResult HomeView(string category, int pageIndx = 1)
        {
            var ViewModel = new ProductListViewModel
            {
                Products = productRepo.Products.OrderBy(p => p.ProductId)
                        .Where(p => category == null || p.Category.CategoryName == category)
                        .Skip((pageIndx - 1) * pageSize)
                        .Take(pageSize),
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
                    productDict.productMap.Add(product.ProductName, product);
                    trie.Insert(product.ProductName.ToLower());
                }
            }
            

            return View(ViewModel);
        }
    }
}
