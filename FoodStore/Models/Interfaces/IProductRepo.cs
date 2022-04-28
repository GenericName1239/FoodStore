using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Models.Interfaces
{
    public interface IProductRepo
    {
        IQueryable<Product> Products { get; }
    }
}
