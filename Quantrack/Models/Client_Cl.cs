using MySql.Data.MySqlClient;
using Npgsql;

namespace Quantrack.Models
{
    public class Client_Cl
    {
        public int Id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public bool AddClient()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new MySqlCommand("insert into client(prenom,nom,phone,email) values " +
                    "(@prenom,@nom,@phone,@email)", cnx);
                cm.Parameters.AddWithValue("@prenom", this.Prenom);
                cm.Parameters.AddWithValue("@nom", this.Nom);
                cm.Parameters.AddWithValue("@phone", this.Phone);
                cm.Parameters.AddWithValue("@email", this.Email);

                var i = cm.ExecuteNonQuery();
                if (i != 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
