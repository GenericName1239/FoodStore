using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FoodStore.Pages.Admin
{
    [Authorize(Roles = "administrator")]
    public class AdminModel : PageModel
    {
        private UserManager<IdentityUser> userManager;

        [BindProperty]
        [Required(ErrorMessage = "Username is required!")]
        public string UserName { get; set; }

        [BindProperty]
        public string Claim { get; set; }

        [BindProperty]
        public bool Delete { get; set; }

        public void OnGet()
        {
        }


        public AdminModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(UserName);

                if(user == null) { 
                    ModelState.AddModelError("UserDontExist", "This user dosn't exist!"); 
                }
                else
                {
                    if (Delete) { await userManager.DeleteAsync(user); }
                    else
                    {
                        await userManager.AddClaimAsync(user, new Claim(Claim, "true"));
                    }
                }              
            }

            return Page();
        }
    }
}
