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
using System.Net.Mail;

namespace CodeWithMukesh.Areas.Admin.Pages.Users
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IViewRenderService _viewRenderService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IViewRenderService viewRenderService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _viewRenderService = viewRenderService;
        }
        public async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnGetLoadAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var models = new List<ApplicationUserModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                models.Add(new ApplicationUserModel { Id = user.Id, Email = user.Email, EmailConfirmed = user.EmailConfirmed, Roles = roles });
            }

            return Partial("_ViewAll", models);
        }

        public async Task<IActionResult> OnGetCreateUserAsync()
        {
            return await Task.FromResult(Partial("_Create", new CreateUserInput { }));
        }

        public async Task<IActionResult> OnPostCreateUserAsync(CreateUserInput input)
        {            
            string userName = input.Email;
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = input.Email,

                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(user, input.Password);
            if (!result.Succeeded)
            {
                var html = await _viewRenderService.RenderViewToStringAsync("./_CreateUserError", result.Errors );
                return new JsonResult(new { success = false, errors = result.Errors, html =html });
                
            }
            await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
            await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return new JsonResult(new { success = true, redirect= $"/admin/users/userRole?userId={user.Id}"});
    
        }
    }

    public class ApplicationUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class CreateUserInput
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}