using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public List<RoleViewModel> RoleViewModelList {get;set;}

        public IndexModel(UserManager<ApplicationUser> userManager,
                          RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task OnGetAsync()
        {
            System.Console.WriteLine("Roles  On Get............");
            await LoadModel();
        }

        public async Task<IActionResult> OnGetViewAllAsync()
        {
            await LoadModel();
            return Partial("_ViewAll", RoleViewModelList);
            
        }

        private async Task LoadModel()
        {
            RoleViewModelList = await _roleManager.Roles.Select(f=> new RoleViewModel{ Name= f.Name, Id= f.Id }).ToListAsync(); 
        }

    }

    public class RoleViewModel
    {
        [Required]        public string Name {get;set;}
        public string Id {get;set;}
    }
}