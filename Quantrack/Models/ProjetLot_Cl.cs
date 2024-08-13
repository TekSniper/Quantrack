using MySql.Data.MySqlClient;
using Npgsql;

namespace Quantrack.Models
{
    public class ProjetLot_Cl
    {
        public int Id { get; set; }
        public int ProjetId { get; set; }
        public int LotId { get; set; }

        public bool AddProjetLot()
        {
            using(var cnx=new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new NpgsqlCommand("AddProjetLot", cnx);
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                //MySqlParameter value = new MySqlParameter("projetid", ProjetId);
                cm.Parameters.Add(new NpgsqlParameter("projetid",NpgsqlTypes.NpgsqlDbType.Integer, ProjetId));
                cm.Parameters.Add(new NpgsqlParameter("lotid", NpgsqlTypes.NpgsqlDbType.Integer, LotId));

                var  i = cm.ExecuteNonQuery();  
                if(i!=0) 
                    return true;
                else
                    return false;
            }
        }
    }
}
