using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.ExtensionMethods
{
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest httpRequest)
        {
            return httpRequest.QueryString.HasValue ? $"{httpRequest.Path} {httpRequest.Query}" 
                : httpRequest.Path.ToString();
        }
    }
}
