using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FoodStore.ExtensionMethods;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodStore.Pages.Register
{
    public class RegisterModel : PageModel
    {
        private UserManager<IdentityUser> userManager;

        [BindProperty]
        [Required(ErrorMessage = "Enter valid username!")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter valid email!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter valid password!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Confirmed password is required!")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = Name,
                    Email = Email,
                    EmailConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(user, Password);
                result.AddIdentityErrors(ModelState);

                if(result.Succeeded)
                {
                    return RedirectToPage("/Login/SignIn");
                }
            }

            return Page();
        }
    }
}
