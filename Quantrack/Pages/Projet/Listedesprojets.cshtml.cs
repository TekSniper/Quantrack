using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Quantrack.Pages.Projet
{
    public class ListedesprojetsModel : PageModel
    {
        public string LaSession { get; set; } = string.Empty;
        public void OnGet()
        {
            LaSession = HttpContext.Session.GetString("Login")!;
        }
    }
}
