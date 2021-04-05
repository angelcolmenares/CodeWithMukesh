using System;
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

namespace CodeWithMukesh.Areas.Admin.Pages.Test
{
     [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IViewRenderService _viewRenderService;

        public List<RoleViewModel> RoleViewModelList {get;set;}
        public RoleViewModel RoleViewModel {get;set;}

        public string ViewMode {get;set;} ="ViewAll";

        public IndexModel(UserManager<ApplicationUser> userManager,
                          RoleManager<IdentityRole> roleManager,
                          IViewRenderService viewRenderService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _viewRenderService = viewRenderService;
        }

        public async Task OnGetAsync()
        {
            System.Console.WriteLine("Roles  On Get............");
            await LoadRoleViewModelList();
        }

        public async Task<IActionResult> OnGetViewAllAsync()
        {                    
            await LoadRoleViewModelList();
            return Partial("_ViewAll", RoleViewModelList)  ;
        }

        public async Task<IActionResult> OnGetEditAsync(string roleId)
        {
            ViewMode="EditRole";
            await LoadRoleViewModel(roleId);
            return  await Task.FromResult( Page());
        }

        public async Task<IActionResult> OnPostEditAsync(RoleViewModel input)
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

            var role =  await  _roleManager.Roles.FirstOrDefaultAsync(f=> f.Id== input.Id);            
            if( role.Name== CodeWithMukesh.Auth.Roles.SuperAdmin.ToString())
            {
                var errors = new List<IdentityError>( );
                errors.Add( new IdentityError{ Description="Superamdin", Code="no"});
                var html = await _viewRenderService.RenderViewToStringAsync("_IdentityError", errors );
                return new JsonResult(new { success = false, errors = errors, html =html });                
            }

            await _roleManager.SetRoleNameAsync( role, input.Name);
            await _roleManager.UpdateAsync(role);            
            return new JsonResult(new { success = true, redirect= $"/admin/test"});            
        }

        private async Task LoadRoleViewModelList()
        {
            RoleViewModelList = await _roleManager.Roles.Select(f=> new RoleViewModel{ Name= f.Name, Id= f.Id }).ToListAsync(); 
        }

        private async Task LoadRoleViewModel(string roleId)
        {
            RoleViewModel = await _roleManager.Roles.Select(f=> new RoleViewModel
            {
                Id= f.Id,
                Name = f.Name
            })
            .FirstOrDefaultAsync(f=> f.Id== roleId);
        }
    }

    public class RoleViewModel
    {
        [Required]        public string Name {get;set;}
        public string Id {get;set;}
    }
}