using MySql.Data.MySqlClient;
using Npgsql;

namespace Quantrack.Models
{
    public class Projet_Cl
    {
        public int Id { get; set; }
        public int ServideId { get; set; }
        public string NomProjet { get; set; }
        public string Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string Statut { get; set; }
        public decimal Montant { get; set; }
        public string Version { get; set; }

        public bool CreateProjet()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new NpgsqlCommand("CreateProjet", cnx);
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.Add(new NpgsqlParameter("v_service", this.ServideId));
                cm.Parameters.Add(new NpgsqlParameter("v_nom", this.NomProjet));
                cm.Parameters.Add(new NpgsqlParameter("v_descri", this.Description));
                cm.Parameters.Add(new NpgsqlParameter("v_dated", this.DateDebut));
                cm.Parameters.Add(new NpgsqlParameter("v_datef", this.DateFin));
                cm.Parameters.Add(new NpgsqlParameter("v_statut", this.Statut));
                cm.Parameters.Add(new NpgsqlParameter("v_vserion", this.Version));
                cm.Parameters.Add(new NpgsqlParameter("v_montant", this.Montant));

                var i = cm.ExecuteNonQuery();
                if (i != 0)
                    return true;
                else
                    return false;
            }
        }
        public bool ChangeStatus()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new NpgsqlCommand("update projet set statut=@statut where id=@id", cnx);
                cm.Parameters.AddWithValue("@statut",this.Statut);
                cm.Parameters.AddWithValue("@id", this.Id);

                var  i = cm.ExecuteNonQuery();
                if(i != 0) return true;
                else return false;
            }
        }
        public bool ChangerVersion()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new NpgsqlCommand("update projet set version_p=@version where id=@id", cnx);
                cm.Parameters.AddWithValue("@version", this.Statut);
                cm.Parameters.AddWithValue("@id", this.Id);

                var i = cm.ExecuteNonQuery();
                if (i != 0) return true;
                else return false;
            }
        }
    }
}
