using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using CodeWithMukesh.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace CodeWithMukesh.Areas.Admin.Pages.Users
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager){
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task OnGetAsync()
        {
            await Task.CompletedTask;
            //Users= await _userManager.Users.ToListAsync();             
        }

        public async Task<IActionResult> OnGetLoadAllAsync()
        {            
            var users = await _userManager.Users.ToListAsync();   
            var models = new List<ApplicationUserModel>();
            foreach( var user in users)
            {
                var roles = await _userManager.GetRolesAsync( user);
                models.Add( new ApplicationUserModel{ Id= user.Id, Email= user.Email, EmailConfirmed= user.EmailConfirmed, Roles= roles });
            }
                    
            return Partial("_ViewAll", models);
        }
    }

    public class ApplicationUserModel
    {
        public string Id {get;set;}
        public string Email {get;set;}
        public bool EmailConfirmed {get;set;}
        public IList<string> Roles {get;set;}
    }
}