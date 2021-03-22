using CodeWithMukesh.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeWithMukesh.Areas.Admin.Pages.UserRole
{    
    public class UserRoleModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [BindProperty] public ManageUserRolesViewModel UserRolesViewModel { get;  set; }

        public async Task OnGetAsync(string userId)
        {
            var viewModel = new List<UserRolesViewModel>();
            var user = await _userManager.FindByIdAsync(userId);
            ViewData["Title"] = $"{user.UserName} - Roles";
            ViewData["Caption"] = $"Manage {user.Email}'s Roles.";
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                viewModel.Add(userRolesViewModel);
            }
            UserRolesViewModel = new ManageUserRolesViewModel()
            {
                UserId = userId,
                UserRoles = viewModel
            };

            
        }

        public async Task<IActionResult> OnPostUpdateAsync(string id)
        {
            
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);            
            result = await _userManager.AddToRolesAsync(user, UserRolesViewModel.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));            
            return Redirect("/admin/users"); // RedirectToAction("Index", new { userId = id });
        }
    }

    public class ManageUserRolesViewModel
    {
        public ManageUserRolesViewModel()
        {
        }

        public string UserId { get; set; }
        public List<UserRolesViewModel> UserRoles { get; set; }
    }

    public class UserRolesViewModel
    {
        public string RoleName { get;  set; }
        public bool Selected { get;  set; }
    }
}