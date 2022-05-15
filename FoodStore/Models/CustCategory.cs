using System;
using System.Collections.Generic;

#nullable disable

namespace FoodStore.Models
{
    public partial class CustCategory
    {
        public CustCategory()
        {
            Customers = new HashSet<Customer>();
        }


        public string CustCategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
