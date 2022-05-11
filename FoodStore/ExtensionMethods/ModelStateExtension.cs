using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.ExtensionMethods
{
    public static class ModelStateExtension
    {
        public static void AddIdentityErrors(this IdentityResult identityResult, ModelStateDictionary modelState)
        {
            foreach(var error in identityResult.Errors)
            {
                modelState.AddModelError(error.Code, error.Description);
            }
        }
    }
}
