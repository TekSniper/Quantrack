using MySql.Data.MySqlClient;
using Npgsql;

namespace Quantrack.Models
{
    public class Client_Cl
    {
        public int Id { get; set; }
        public string Prenom { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public bool AddClient()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new MySqlCommand("AddClient", cnx);
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("v_prenom", this.Prenom);
                cm.Parameters.AddWithValue("v_nom", this.Nom);
                cm.Parameters.AddWithValue("v_phone", this.Phone);
                cm.Parameters.AddWithValue("v_email", this.Email);

                var i = cm.ExecuteNonQuery();
                if (i != 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
