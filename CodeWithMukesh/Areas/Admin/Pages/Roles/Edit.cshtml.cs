using CodeWithMukesh.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeWithMukesh.Areas.Admin.Pages.Roles
{
    public class EditRole : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IViewRenderService _viewRenderService;

        public RoleViewModel RoleViewModel { get; set; }


        public EditRole(
            RoleManager<IdentityRole> roleManager,
            IViewRenderService viewRenderService)
        {
            _roleManager = roleManager;
            _viewRenderService = viewRenderService;
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
            if(!ModelState.IsValid)
            {
                var errors=  ModelState.Where(x=>x.Value.Errors.Count>0)
                .SelectMany( state=>  state.Value.Errors, (state, error)=> 
                new Tuple<string  ,string>(state.Key, error.ErrorMessage) )
                .ToList<Tuple<string ,string>>();                
                
                
                var html = await _viewRenderService.RenderViewToStringAsync("_ModelError", errors );

                return new JsonResult(new { success = false, errors = errors, html =html });                
            }
            var role =  await  _roleManager.Roles
            .FirstOrDefaultAsync(f=> f.Id== input.Id);
            
            if( role.Name== CodeWithMukesh.Auth.Roles.SuperAdmin.ToString())
            {
                var errors = new List<IdentityError>( );
                errors.Add( new IdentityError{ Description="Superamdin", Code="no"});
                var html = await _viewRenderService.RenderViewToStringAsync("_IdentityError", errors );
                return new JsonResult(new { success = false, errors = errors, html =html });                
            }

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