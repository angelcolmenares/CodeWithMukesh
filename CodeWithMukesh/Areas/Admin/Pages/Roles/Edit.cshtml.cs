using CodeWithMukesh.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeWithMukesh.Areas.Admin.Pages.Roles
{
    public class EditRole : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleViewModel RoleViewModel { get; set; }


        public EditRole(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task OnGet(string roleId)
        {            
            await  LoadModel(roleId);
        }

        public async Task<IActionResult> OnGetLoadAsync(string roleId)
        {
            await LoadModel(roleId);
            return Partial("_Edit", RoleViewModel);
        }

        public async Task<IActionResult> OnPostSaveAsync(RoleViewModel input)
        {
            System.Console.WriteLine($"id : {input?.Id}  name = {input.Name}");            
            var role =  await  _roleManager.Roles
            .FirstOrDefaultAsync(f=> f.Id== input.Id);
            System.Console.WriteLine($"role id : {role?.Id}  role name = {role.Name}");            
            await _roleManager.SetRoleNameAsync( role, input.Name);
            await _roleManager.UpdateAsync(role);
            return new JsonResult(new { success = true, redirect= $"/admin/roles"});
    
        }

        private async Task LoadModel(string roleId)
        {
            RoleViewModel = await _roleManager.Roles.Select(f=> new RoleViewModel
            {
                Id= f.Id,
                Name = f.Name
            })
            .FirstOrDefaultAsync(f=> f.Id== roleId);

        }

    }

}