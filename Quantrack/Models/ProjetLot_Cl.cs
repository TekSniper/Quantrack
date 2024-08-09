using MySql.Data.MySqlClient;

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
                var cm = new MySqlCommand("AddProjetLot", cnx);
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                //MySqlParameter value = new MySqlParameter("projetid", ProjetId);
                cm.Parameters.Add(new MySqlParameter("projetid",MySqlDbType.Int32, ProjetId));
                cm.Parameters.Add(new MySqlParameter("lotid",MySqlDbType.Int32, LotId));

                var  i = cm.ExecuteNonQuery();  
                if(i!=0) 
                    return true;
                else
                    return false;
            }
        }
    }
}
