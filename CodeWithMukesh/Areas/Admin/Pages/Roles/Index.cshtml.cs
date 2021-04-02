using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeWithMukesh.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CodeWithMukesh.Areas.Admin.Pages.Roles
{
     [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(UserManager<ApplicationUser> userManager,
                          RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task OnGet()
        {
            System.Console.WriteLine("Roles  On Get............");
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnGetViewAllAsync()
        {
            var model = await _roleManager.Roles.Select(f=> new RoleViewModel{ Name= f.Name, Id= f.Id }).ToListAsync();            
            return Partial("_ViewAll", model);
            
        }

    }

    public class RoleViewModel
    {
        public string Name {get;set;}
        public string Id {get;set;}
    }
}