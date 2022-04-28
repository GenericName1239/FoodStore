using System;
using System.Collections.Generic;

#nullable disable

namespace FoodStore.Models
{
    public partial class CustHistory
    {
        public string CustomerId { get; set; }
        public string CategoryId { get; set; }
        public DateTime? RegDate { get; set; }

        public virtual CustCategory Category { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
