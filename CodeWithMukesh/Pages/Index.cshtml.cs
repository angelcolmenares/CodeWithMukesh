using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CodeWithMukesh.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            System.Console.WriteLine("OnGet");
        }

        public async Task<IActionResult> OnGetLoadMenu()
        {
            return await Task.FromResult( Partial("_AppMenu"));

        }

        public async Task<IActionResult> OnGetLoadPage(string module, string pageName, bool reload)
        {
            System.Console.WriteLine($"On Get Module:'{module}' pageName:'{pageName}', {reload} ");
            await Task.CompletedTask;
            if(reload)
            {
                System.Console.WriteLine($"redirect");
                return Redirect("/");
            }

            if(!string.IsNullOrEmpty(pageName) && pageName!="/Index" )
            {
                System.Console.WriteLine($"partial: '{pageName}'");
                return Partial($"_{pageName}");
            }

            System.Console.WriteLine($"redirect _myPartial");
            
            return Partial("_AppMenu");

        }
    }
}
