using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Models.ViewModels
{
    public class PageInfo
    {
        public int CurrentPage { get; set; }
        public int TotalProducts { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling((decimal)TotalProducts / PageSize);
    }
}
