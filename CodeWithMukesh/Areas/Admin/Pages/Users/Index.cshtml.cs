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
        public List<ApplicationUser> Users = new List<ApplicationUser>();

        public IndexModel(UserManager<ApplicationUser> userManager){
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            Users= await _userManager.Users.ToListAsync();             
        }

        public async Task<IActionResult> OnGetLoadAllAsync()
        {            
            Users = await _userManager.Users.ToListAsync();            
            return Partial("_ViewAll", Users);
        }
    }
}