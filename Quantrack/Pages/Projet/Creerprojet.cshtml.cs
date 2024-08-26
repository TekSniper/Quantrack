using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using MySql.Data.MySqlClient;
using Quantrack.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Quantrack.Pages.Projet
{
    public class CreerprojetModel : PageModel
    {
        public string SuccessMessage { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public string WarningMessage { get; set; } = string.Empty;
        public string PageTitle { get; set; } = "Creation du projet";
        public string PageIcon { get; set; } = "fa-solid fa-diagram-project";
        public string LoginUser { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public int NumPanier { get; set; }
        public Panier_Cl panier = new Panier_Cl();
        public List<Service_Cl> services { get; set; }
        public void OnGet()
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                Response.Redirect("/Authentification");
                return;
            }
            else
            {
                this.LoginUser = HttpContext.Session.GetString("Login")!;
                this.UserType = HttpContext.Session.GetString("Type")!;
                //var panier = new Panier_Cl();
                panier.LoginUser = this.LoginUser;
                this.NumPanier = panier.CountInCart();

                /*
                using (var cnx = new DbConnexion().GetConnection())
                {
                    cnx.Open();
                    var cm = new MySqlCommand("select * from service", cnx);
                    services = new List<Service_Cl>();
                    var reader = cm.ExecuteReader();
                    while (reader.Read())
                    {
                        var service = new Service_Cl();
                        service.Id = reader.GetInt32(0);
                        service.Designation = reader.GetString(1);

                        services.Add(service);
                    }
                }*/
            }
        }

        
        public List<Service_Cl> GetServices()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new MySqlCommand("select * from service", cnx);
                services = new List<Service_Cl>();
                var reader = cm.ExecuteReader();
                while (reader.Read())
                {
                    var service = new Service_Cl();
                    service.Id = reader.GetInt32(0);
                    service.Designation = reader.GetString(1);

                    services.Add(service);
                }

                return services;
            }
        }
        public void OnPost()
        {
            panier.ServideId = int.Parse(Request.Form["service"]!);
            panier.NomProjet = Request.Form["nomprojet"].ToString()!;
            panier.Description = Request.Form["description"].ToString()!;
            panier.DateDebut = DateTime.Parse(Request.Form["date_d"].ToString()!);
            panier.DateFin = DateTime.Parse(Request.Form["date_f"].ToString()!);
            panier.Statut = "Nouveau";
            panier.LoginUser = HttpContext.Session.GetString("Login")!;
            panier.Montant = decimal.Parse(Request.Form["montant"].ToString()!);
            if(
                panier.ServideId == 0 || panier.NomProjet == string.Empty || panier.Description==string.Empty || panier.DateDebut == DateTime.MinValue || 
                panier.DateFin == DateTime.MinValue || panier.Statut == string.Empty || panier.LoginUser == string.Empty || panier.Montant == 0.00m
                )
            {
                WarningMessage = "Replissez les vides s'il vous plait !";
                return;
            }
            else
            {
                try
                {
                    var isAdded = panier.AddToCart();
                    switch (isAdded)
                    {
                        case true:
                            {
                                SuccessMessage = "Le projet est ajouté au panier. Vous pouvez ajouter d'autres et avoir un lot de deux ou trois plusieurs projets du client.";
                                //panier.ServideId = 0;
                                panier.NomProjet = string.Empty;
                                panier.Description = string.Empty;
                                panier.DateDebut = DateTime.MinValue;
                                panier.DateFin = DateTime.MinValue;
                                panier.Statut = string.Empty;
                                panier.LoginUser = string.Empty;
                                panier.Montant = 0.00m;
                                this.LoginUser = HttpContext.Session.GetString("Login")!;
                                this.UserType = HttpContext.Session.GetString("Type")!;
                                OnGet();
                            }
                            break;
                        case false:
                            {
                                ErrorMessage = "Echec de l'ajout du projet dans le panier !";
                            }
                            break;
                    }
                }
                catch ( Exception ex )
                {
                    ErrorMessage = ex.Message;
                }                
            }            
        }
    }
}
