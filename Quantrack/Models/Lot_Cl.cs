using MySql.Data.MySqlClient;
using Npgsql;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quantrack.Models
{
    public class Lot_Cl
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime DateCreation { get; set; }
        public string Statut { get; set; } = string.Empty;
        public int UserId { get; set; }

        public bool CreateLot()
        {
            using(var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new MySqlCommand("insert into lot(client_id,datecreation,statut,user_id) values " +
                    "(@client,@date,@statut,@user)", cnx);
                cm.Parameters.AddWithValue("@client", this.ClientId);
                cm.Parameters.AddWithValue("@date", this.DateCreation);
                cm.Parameters.AddWithValue("@statut", this.Statut);
                cm.Parameters.AddWithValue("@user", this.UserId);

                var i = cm.ExecuteNonQuery();
                if (i != 0)
                    return true;
                else
                    return false;
            }
        }
        public bool ChangeStatus()
        {
            using(var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new MySqlCommand("update lot set statut=@statut where id=@id", cnx);
                cm.Parameters.AddWithValue("@statut", Statut);
                cm.Parameters.AddWithValue("@id", Id);

                var i = cm.ExecuteNonQuery();
                if (i != 0) return true;
                else return false;
            }
        }
    }
}
