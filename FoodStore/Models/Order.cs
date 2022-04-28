using System;
using System.Collections.Generic;

#nullable disable

namespace FoodStore.Models
{
    public partial class Order
    {
        public Order()
        {
            ProductOrders = new HashSet<ProductOrder>();
        }

        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
