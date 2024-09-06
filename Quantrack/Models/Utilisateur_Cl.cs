using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using Npgsql;

namespace Quantrack.Models
{
    public class Utilisateur_Cl
    {        
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom {  get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string MotDePasse { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public byte Statut { get; set; }

        public bool ExistsOneOrMorUsers()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new MySqlCommand("select count(*) from utilisateur", cnx);
                var reader = cm.ExecuteReader();
                var check = false;
                if (reader.Read())
                {
                    if (reader.GetInt32(0) == 0)
                        check = false;
                    else
                        check = true;
                }

                return check;
            }
        }
        public bool ExistsUser()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new MySqlCommand("select * from utilisateur where login=@login", cnx);
                cm.Parameters.AddWithValue("@login", this.Login.ToLower());
                var reader = cm.ExecuteReader();
                if (reader.Read())
                    return true;
                else
                    return false;
            }
        }
        public bool EtatUser()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new MySqlCommand("select * from utilisateur where statut=@status and login=@login", cnx);

                cm.Parameters.AddWithValue("@status", 1);
                cm.Parameters.AddWithValue("@login", this.Login.ToLower());
                var reader = cm.ExecuteReader();
                if(reader.Read())
                    return true;
                else
                    return false;
            }
        }
        public bool Authentification()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new MySqlCommand("select * from utilisateur where login=@login and mot_de_passe=@pwd", cnx);
                //cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@login", this.Login.ToLower());
                cm.Parameters.AddWithValue("@pwd",this.MotDePasse);
                var reader = cm.ExecuteReader();
                if (reader.Read())
                    return true;
                else
                    return false;
            }
        }
        public string GetTypeUser()
        {
            using(var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new MySqlCommand("select * from utilisateur where login=@login", cnx);
                cm.Parameters.AddWithValue("@login", Login.ToLower());
                var reader = cm.ExecuteReader();
                if(reader.Read())
                    this.Type = reader.GetString(5);

                return this.Type;
            }
        }
        public int GetIdUser()
        {
            using(var cnx=new DbConnexion().GetConnection())
            {
                cnx.Open();
                var cm = new MySqlCommand("GetIdUser", cnx);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("v_login",Login.ToLower());
                var reader = cm.ExecuteReader();
                if( reader.Read())
                    this.Id = reader.GetInt32(0);

                return this.Id; 
            }
        }
    }
}
