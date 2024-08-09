using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quantrack.Models;

namespace Quantrack.Pages
{
    public class AuthentificationModel : PageModel
    {
        public string SuccessMessage { get; set; } = string.Empty;
        public string WarningMessage { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public Utilisateur_Cl user = new Utilisateur_Cl();
        private string LoginSession { get; set; } = string.Empty;
        public void OnGet()
        {
            var checkExists = user.ExistsOneOrMorUsers();
            if (checkExists)
            {
                this.LoginSession = HttpContext.Session.GetString("Login")!;
                if (LoginSession == null){

                }
                else
                    Response.Redirect("/Projet/Listedesprojets");
            }
            else
                Response.Redirect("/Creerutilisateur");
        }
        public void OnPost() 
        {
            user.Login = Request.Form["login"].ToString();
            user.MotDePasse = Request.Form["pwd"].ToString();
            if (user.Login == string.Empty || user.MotDePasse == string.Empty) 
            {
                WarningMessage = "Remplissez les vides svp !";
                return; 
            }
            else
            {
                var existsUser = user.ExistsUser();
                if (!existsUser)
                {
                    ErrorMessage = "Cet utilisateur n'existe pas dans le système !";
                    return;
                }
                else
                {
                    var isActivated = user.EtatUser();
                    if (!isActivated)
                    {
                        WarningMessage = "Cet utilisateur est désactivé. Veuillez contacter l'administrateur";
                    }
                    else
                    {
                        var authenticated = user.Authentification();
                        switch (authenticated)
                        {
                            case true:
                                {
                                    HttpContext.Session.SetString("Login", user.Login);
                                    HttpContext.Session.SetString("Type", user.GetTypeUser());
                                    Response.Redirect("/Projet/Listedesprojets");
                                }
                                break;
                            case false:
                                {
                                    WarningMessage = "Login ou mot de passe incorrect !";
                                }
                                break;
                        }
                    }
                }
            }            
        }    
    }
}
