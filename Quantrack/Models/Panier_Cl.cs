using MySql.Data.MySqlClient;
using Npgsql;

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
        public decimal Montant { get; set; }
        public string LoginUser { get; set; }

        public bool AddToCart()
        {
            using (var cnx=new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new NpgsqlCommand("AddToCart", cnx);
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.Add(new MySqlParameter("v_service", this.ServideId));
                cm.Parameters.Add(new MySqlParameter("v_nom", this.NomProjet));
                cm.Parameters.Add(new MySqlParameter("v_descri", this.Description));
                cm.Parameters.Add(new MySqlParameter("v_dated", this.DateDebut));
                cm.Parameters.Add(new MySqlParameter("v_datef", this.DateFin));
                cm.Parameters.Add(new MySqlParameter("v_statut", this.Statut));
                cm.Parameters.Add(new MySqlParameter("v_user", this.LoginUser));
                cm.Parameters.Add(new MySqlParameter("v_montant", this.Montant));

                var i = cm.ExecuteNonQuery();
                if (i != 0)
                    return true;
                else
                    return false;
            }
        }
        public int CountInCart()
        {
            using(var cnx =  new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new NpgsqlCommand("select count(*) from panier p where p.user_login = @login", cnx);
                cm.Parameters.AddWithValue("@login", this.LoginUser);
                var reader = cm.ExecuteReader();
                var c = 0;
                if(reader.Read())
                    c = reader.GetInt32(0);


                return c;
            }
        }
        public bool DeleteAllInCartForUser()
        {
            using(var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new NpgsqlCommand("delete from panier  where user_login=@login", cnx);
                cm.Parameters.AddWithValue("@login", LoginUser);
                var i = cm.ExecuteNonQuery();

                if (i != 0) return true;
                else return false;
            }
        }
        public bool RemoveItemInCart()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new NpgsqlCommand("delete from panier where user_login=@login and id=@id", cnx);
                cm.Parameters.AddWithValue("@login", this.LoginUser);
                cm.Parameters.AddWithValue("@id", this.Id);
                var i = cm.ExecuteNonQuery();

                if (i != 0) return true;
                else return false;
            }
        }
    }
}
