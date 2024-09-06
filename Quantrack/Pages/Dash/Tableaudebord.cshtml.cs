using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Npgsql;
using Quantrack.Models;
using Quantrack.Pages.Projet;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

namespace Quantrack.Pages.Dash
{
    public class TableaudebordModel : PageModel
    {
        public string LoginUser { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string PageTitle { get; set; } = "Tableau de bord";
        public string PageIcon { get; set; } = "fa-solid fa-gauge-high";
        public string ErrorMessage { get; set; } = string.Empty;
        public int NumPanier { get; set; }
        public List<Projet_Cl> ProjectInProgress { get; set; } = new List<Projet_Cl>();
        public List<Projet_Cl> ProjectToClose { get; set; } = new List<Projet_Cl>();
        public List<Utilisateur_Cl> infoUser { get; set; } = new List<Utilisateur_Cl>();
        public int NbProjectDesign { get; set; }
        public int NbProjectItDev { get; set; }
        public int NbClients { get; set; }
        public void OnGet()
        {
            if(HttpContext.Session.GetString("Login")==null)
            {
                Response.Redirect("/Authentification");
                return;
            }
            else
            {
                try
                {
                    this.LoginUser = HttpContext.Session.GetString("Login")!;
                    this.UserType = HttpContext.Session.GetString("Type")!;
                    var panier = new Panier_Cl();
                    panier.LoginUser = this.LoginUser;
                    this.NumPanier = panier.CountInCart();
                    ProjectInProgress = new TableauDeBord_Cl(LoginUser).GetProjectInProgress();
                    ProjectToClose = new TableauDeBord_Cl(LoginUser).GetProjetToClose();
                    NbProjectDesign = new TableauDeBord_Cl(LoginUser).GetNumDesignCompletedProjects();
                    NbClients = new TableauDeBord_Cl(LoginUser).GetNumClientsCompletedProjets();
                    NbProjectItDev = new TableauDeBord_Cl(LoginUser).GetNumDevCompletedProjetcs();
                }
                catch(Exception e)
                {
                    ErrorMessage = e.Message;
                }
                
                //var connectionString = "";
                //connectionString = new DbConnexion().GetConfiguration().GetSection("ConnectionStrings").GetSection("L€OPGS").Value!;
                //var ds = NpgsqlDataSource.Create(connectionString);
                //using(var cnx = new DbConnexion().GetConnection_1())
                //{
                //    cnx.Open();
                //    using (var cm = new NpgsqlCommand("call insert_user(@1,@2,@3,@4,@5)"))
                //    {
                //        cm.Connection = cnx;
                //        cm.Parameters.AddWithValue("1", "Parousia");
                //        cm.Parameters.AddWithValue("2", "Lubenzo");
                //        cm.Parameters.AddWithValue("3", "p2lix");
                //        cm.Parameters.AddWithValue("4", "parousia");
                //        cm.Parameters.AddWithValue("5", "Admin");
                //        enregistrer = cm.ExecuteNonQuery();
                //    }
                //}                
            }
        }        
    }
}
