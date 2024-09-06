using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using MySql.Data.MySqlClient;
using Quantrack.Models;

namespace Quantrack.Pages.Projet
{
    public class PanierModel : PageModel
    {
        public string PageTitle { get; set; } = "Panier";
        public string PageIcon { get; set; } = "fa-solid fa-cart-shopping";
        public string LoginUser { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public int NumPanier { get; set; }
        public string PrenomClient { get; set; } = string.Empty;
        public string NomClient { get; set; } = string.Empty;
        public string PhoneClient { get; set; } = string.Empty;
        public string EmailClient { get; set; } = string.Empty;
        private int _counter { get; set; } = 0;
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
                var panier = new Panier_Cl();
                panier.LoginUser = this.LoginUser;
                this.NumPanier = panier.CountInCart();
            }
        }
        public int IncrementCounter()
        {
            _counter += 1;
            return _counter;
        }
        public List<Panier_Cl> GetPanier()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new MySqlCommand("select * from panier where login_user=@login", cnx);
                cm.Parameters.AddWithValue("@login", this.LoginUser);
                var reader = cm.ExecuteReader();
                var listPanier = new List<Panier_Cl>();
                while (reader.Read())
                {
                    listPanier.Add(new Panier_Cl
                        {
                            Id = reader.GetInt32(0),
                            ServideId = reader.GetInt32(1),
                            NomProjet = reader.GetString(2),
                            Description = reader.GetString(3),
                            DateDebut = reader.GetDateTime(4),
                            DateFin = reader.GetDateTime(5),
                            Statut = reader.GetString(6),
                            LoginUser = reader.GetString(7),
                            Montant = reader.GetDecimal(8)
                        });
                }
                return listPanier;
            }
        }
    }
}
