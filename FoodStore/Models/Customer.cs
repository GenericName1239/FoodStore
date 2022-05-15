using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace FoodStore.Models
{
    public partial class Customer : IdentityUser
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public string CustCategoryId { get; set; }
        public virtual CustCategory Category { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
