using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodStore.Pages.Login
{
    public class SignInModel : PageModel
    {
        private SignInManager<IdentityUser> signInManager;

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        public SignInModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (Email.Length > 0 && Password.Length > 0)
            {
                Microsoft.AspNetCore.Identity.SignInResult signInResult = await signInManager.PasswordSignInAsync(Email, Password, false, false);

                if (signInResult.Succeeded)
                {
                    return Redirect(ReturnUrl ?? "/");
                }
            }

            return Page();
        }
    }
}
