using System;
using System.Collections.Generic;
using System.Linq;
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

        public void RemoveProduct(Product product)
        {
            CartLines.RemoveAll(p => p.Product.ProductId == product.ProductId);
        }

        public decimal GetTotalProdcutValue() => CartLines.Sum(p => p.Product.ProductPrice * p.Quantity);

        public void Clear() => CartLines.Clear();
    }
}
