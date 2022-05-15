using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodStore.Infrastructure;
using FoodStore.Models;
using FoodStore.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace FoodStore.Pages
{
    public class SearchModel : PageModel
    {
        private Trie trie;
        private ProductDict productDict;
        public List<Product> Products { get; set; }

        public SearchModel(Trie trie, ProductDict productDict)
        {
            this.trie = trie;
            this.productDict = productDict;
            Products = new List<Product>();
        }

        public IActionResult OnGet(string productSearch)
        {
            Product product = null;

            if(productSearch != null)
            {
                productSearch = productSearch.ToLower();

                if (trie.Search(productSearch))
                {
                    productDict.productMap.TryGetValue(productSearch, out product);
                }

                if (product != null)
                {
                    Products.Add(product);
                    return Page();
                }

            }

            return RedirectToAction("HomeView", "Home");
        }

        public JsonResult OnGetSearching(string prefix)
        {
            List<string> response = null;

            if (prefix != null)
            {
               response = trie.FindAllPrefixes(prefix.ToLower());
            }

            return new JsonResult(response);
        }

    }
}
