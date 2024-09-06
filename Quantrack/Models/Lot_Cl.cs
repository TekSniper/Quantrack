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
                var cm = new MySqlCommand("CreateLot", cnx);
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("v_client", this.ClientId);
                cm.Parameters.AddWithValue("v_datec", this.DateCreation);
                cm.Parameters.AddWithValue("v_statut", this.Statut);
                cm.Parameters.AddWithValue("v_user", this.UserId);

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
