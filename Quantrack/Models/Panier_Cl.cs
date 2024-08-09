using MySql.Data.MySqlClient;

namespace Quantrack.Models
{
    public class Panier_Cl
    {
        public int Id { get; set; }
        public int ServideId { get; set; }
        public string NomProjet { get; set; }
        public string Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string Statut { get; set; }
        public string LoginUser { get; set; }

        public bool AddToCart()
        {
            using (var cnx=new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new MySqlCommand("insert into panier(service_id,nomprojet,description,datedebut,datefin,statut,statut,user_login) " +
                    "values (@service,@nomproj,@descrip,@dated,@datef,@statut,@login)", cnx);
                cm.Parameters.AddWithValue("@service", this.ServideId);
                cm.Parameters.AddWithValue("@nomproj", this.NomProjet);
                cm.Parameters.AddWithValue("@descrip", this.Description);
                cm.Parameters.AddWithValue("@dated", this.DateDebut);
                cm.Parameters.AddWithValue("@datef", this.DateFin);
                cm.Parameters.AddWithValue("@statut", this.Statut);
                cm.Parameters.AddWithValue("@login", this.LoginUser);

                var i = cm.ExecuteNonQuery();
                if (i != 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
