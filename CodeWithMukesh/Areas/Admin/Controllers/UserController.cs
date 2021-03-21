using CodeWithMukesh.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeWithMukesh.Areas.Admin.Controllers
{
     [Area("Admin")]
    public class UserController: IdentityControllerBase<UserController>
    {
         private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [Authorize(Policy = IdentityPermissions.Users.View)]
        public IActionResult Index()
        {
            return View();
        }

    }
}