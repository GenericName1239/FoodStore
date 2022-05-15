using FoodStore.Data;
using FoodStore.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodStore.Models
{
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        public List<CartLine> CartLines { get; set; } = new List<CartLine>();
        
        public Cart()
        {
        }

        public void AddProdcut(Product product, int quantity)
        {
            var currentLine = CartLines.Where(p => p.Product.ProductId == product.ProductId).FirstOrDefault();

            if (currentLine == null)
            {
                CartLines.Add(new CartLine { Product = product, Quantity = quantity });
                return;
            }

            currentLine.Quantity += quantity;
        }

        public List<ProductCartViewModel> GetProducts(FoodStoreContext storeContext, IHttpContextAccessor httpContextAccessor)
        {
            List<ProductCartViewModel> cartLines = new List<ProductCartViewModel>();

            var discountProducts = storeContext.Customers.Where(p => p.UserName == httpContextAccessor.HttpContext.User.Identity.Name)
                                 .Join(storeContext.CustCategories, user => user.CustCategoryId,
                                 custCat => custCat.CustCategoryId, (user, custCat) => new { user, custCat })
                                 .Join(storeContext.Offers, _custCat => _custCat.custCat.CustCategoryId,
                                 offer => offer.CustCategoryId, (_custCat, offer) => new { _custCat, offer })
                                 .Join(storeContext.PriceOffers, _offer => _offer.offer.OfferId,
                                 prodOffer => prodOffer.OfferId, (_offer, prodOffer) => new { _offer, prodOffer });


            foreach (var cartLine in CartLines)
            {
                string promoText = "";
                decimal discountPercent = 0;
                var discount = discountProducts.Where(p => p.prodOffer.ProductId == cartLine.Product.ProductId).FirstOrDefault();
                if (discount != null)
                {
                    cartLine.Product.ProductPrice = discount.prodOffer.NewPrice;
                    promoText = discount.prodOffer.PromotionalText;
                    discountPercent = discount._offer.offer.DiscountProcent;
                }

                cartLines.Add(new ProductCartViewModel 
                { 
                    CartLine = cartLine,
                    PromotionalText = promoText,
                    DiscountPercent = discountPercent
                });
            }

            return cartLines;
        }

        public void RemoveProduct(Product product)
        {
            CartLines.RemoveAll(p => p.Product.ProductId == product.ProductId);
        }

        public decimal GetTotalProdcutValue(FoodStoreContext storeContext, IHttpContextAccessor httpContextAccessor) 
            => GetProducts(storeContext, httpContextAccessor).Sum(p => p.CartLine.Product.ProductPrice*p.CartLine.Quantity);

        public void Clear() => CartLines.Clear();
    }
}
