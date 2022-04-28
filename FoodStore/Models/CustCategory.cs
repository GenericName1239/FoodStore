using System;
using System.Collections.Generic;

#nullable disable

namespace FoodStore.Models
{
    public partial class CustCategory
    {
        public CustCategory()
        {
            CustHistories = new HashSet<CustHistory>();
            Customers = new HashSet<Customer>();
        }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<CustHistory> CustHistories { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
