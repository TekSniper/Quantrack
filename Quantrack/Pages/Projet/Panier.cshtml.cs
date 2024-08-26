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
        public int NumPanier { get; set; }
        public string PrenomClient { get; set; } = string.Empty;
        public string NomClient { get; set; } = string.Empty;
        public string PhoneClient { get; set; } = string.Empty;
        public string EmailClient { get; set; } = string.Empty;
        public List<Panier_Cl> listPanier { get; set; }
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
                var cm = new MySqlCommand("select * from panier where user_login=@login", cnx);
                cm.Parameters.AddWithValue("@login", this.LoginUser);
                var reader = cm.ExecuteReader();
                listPanier = new List<Panier_Cl>();
                while (reader.Read())
                {
                    var pan = new Panier_Cl();
                    pan.Id = reader.GetInt32(0);
                    pan.ServideId = reader.GetInt32(1);
                    pan.NomProjet = reader.GetString(2);
                    pan.Description = reader.GetString(3);
                    pan.DateDebut = reader.GetDateTime(4);
                    pan.DateFin = reader.GetDateTime(5);
                    pan.Statut = reader.GetString(6);
                    pan.LoginUser = reader.GetString(7);
                    pan.Montant = reader.GetDecimal(8);

                    listPanier.Add(pan);
                }
                return listPanier;
            }
        }
    }
}
