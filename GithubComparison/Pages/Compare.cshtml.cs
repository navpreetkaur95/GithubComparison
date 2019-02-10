using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GithubComparison.Pages
{
    public class CompareModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Enter the Github usernames you wish to compare";
        }
    }
}
