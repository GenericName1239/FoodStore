using System;
using System.Collections.Generic;

#nullable disable

namespace FoodStore.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustHistories = new HashSet<CustHistory>();
            Orders = new HashSet<Order>();
        }

        public string CustomerId { get; set; }
        public string CategoryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public virtual CustCategory Category { get; set; }
        public virtual ICollection<CustHistory> CustHistories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
