using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quantrack.Models;
using Quantrack.Pages.Projet;

namespace Quantrack.Pages.Dash
{
    public class TableaudebordModel : PageModel
    {
        public string LoginUser { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string PageTitle { get; set; } = "Tableau de bord";
        public int NumPanier { get; set; }
        public void OnGet()
        {
            if(HttpContext.Session.GetString("Login")==null)
            {
                Response.Redirect("/Authentification");
                return;
            }
            else
            {
                this.LoginUser = HttpContext.Session.GetString("Login")!;
                this.UserType = HttpContext.Session.GetString("Type")!;
                var panier = new Panier_Cl();
                panier.LoginUser = this.LoginUser;
                this.NumPanier = panier.CountInCart();
            }
        }
    }
}
