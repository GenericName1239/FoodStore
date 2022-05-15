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
     
        public static void AddIdentityErrors(this SignInResult signInResult, string userName, ModelStateDictionary modelState)
        {
            if (signInResult.IsLockedOut)
            {
                modelState.AddModelError("UserLockedOut", userName + " is locked out.");
            }
            if(signInResult.IsNotAllowed)
            {
                modelState.AddModelError("UserNotAllowed", userName + " is not allowed.");
            }
            else
            {
                modelState.AddModelError("LoginFailed","Something went wrong.");
            }
        }
    }
}
