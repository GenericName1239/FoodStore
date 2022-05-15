using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FoodStore.ExtensionMethods;
using FoodStore.Infrastructure;
using FoodStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodStore.Pages.Login
{
    public class SignInModel : PageModel
    {
        private SignInManager<Customer> signInManager;
        private UserManager<Customer> userManager;
        private Trie trie;

        [BindProperty]
        [Required(ErrorMessage = "Incorrect username or password.")]
        public string UserName{ get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Incorrect username or password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        public SignInModel(SignInManager<Customer> signInManager, UserManager<Customer> userManager, Trie trie)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.trie = trie;
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult signInResult = await signInManager.PasswordSignInAsync(UserName, Password, false, false);
                signInResult.AddIdentityErrors(UserName, ModelState);

                if (signInResult.Succeeded)
                {
                    foreach(var customer in userManager.Users)
                    {
                        trie.Insert("user" + customer.UserName.ToLower());
                    }

                    return Redirect(ReturnUrl ?? "/");
                }
            }

            return Page();
        }
    }
}
