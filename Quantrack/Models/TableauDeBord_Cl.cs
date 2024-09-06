using MySql.Data.MySqlClient;

namespace Quantrack.Models
{
    public class TableauDeBord_Cl
    {
        public string LoginUtilisateur { get; set; }
        public TableauDeBord_Cl(string Login) 
        {
            this.LoginUtilisateur = Login;
        }
        public List<Projet_Cl> GetProjectInProgress()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                //var User = this.LoginUtilisateur;
                var projets = new List<Projet_Cl>();
                using (var cm = new MySqlCommand("ProjetsEnCours", cnx))
                {
                    cm.CommandType = System.Data.CommandType.StoredProcedure;
                    var user = new Utilisateur_Cl();
                    user.Login = LoginUtilisateur;
                    cm.Parameters.AddWithValue("v_user", user.GetIdUser());
                    cm.Parameters.AddWithValue("v_statut", "en cours");
                    var reader = cm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            projets.Add(new Projet_Cl
                            {
                                Id = reader.GetInt32(0),
                                ServideId = reader.GetInt32(1),
                                NomProjet = reader.GetString(2),
                                DateFin = reader.GetDateTime(5)
                            });
                        }
                    }
                    else
                    {

                    }
                    
                }
                return projets;
            }
        }
        public List<Projet_Cl> GetProjetToClose()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var User = new Utilisateur_Cl();

                User.Login = LoginUtilisateur;
                var projets = new List<Projet_Cl>();
                using (var cm = new MySqlCommand("ProjetACloturer", cnx))
                {
                    cm.CommandType = System.Data.CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("v_user", User.GetIdUser());
                    cm.Parameters.AddWithValue("v_statut", "en cours");
                    var reader = cm.ExecuteReader();
                    while (reader.Read())
                    {
                        projets.Add(new Projet_Cl
                        {
                            Id = reader.GetInt32(0),
                            ServideId = reader.GetInt32(1),
                            NomProjet = reader.GetString(2),
                            DateFin = reader.GetDateTime(5)
                        });
                    }
                }
                return projets;
            }
        }
        public int GetNumDevCompletedProjetcs()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var number = 0;
                using (var cm = new MySqlCommand("select GetNumDevCompletedProjetcs(@statut,@service)", cnx))
                {
                    cm.Parameters.AddWithValue("@statut", "Clôturé");
                    cm.Parameters.AddWithValue("@service", 3);
                    number = (int)cm.ExecuteScalar();
                }
                return number;
            }
        }
        public int GetNumDesignCompletedProjects()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var number = 0;
                using (var cm = new MySqlCommand("select GetNumDesignCompletedProjects(@statut,@service)", cnx))
                {
                    cm.Parameters.AddWithValue("@statut", "Clôturé");
                    cm.Parameters.AddWithValue("@service", 2);
                    number = (int)cm.ExecuteScalar();
                }
                return number;
            }
        }
        public int GetNumClientsCompletedProjets()
        {
            using (var cnx = new DbConnexion().GetConnection())
            {
                cnx.Open();
                var number = 0;
                using (var cm = new MySqlCommand("select GetNumClientsCompletedProjets(@statut)", cnx))
                {
                    cm.Parameters.AddWithValue("@statut", "Clôturé");
                    number = (int)cm.ExecuteScalar();
                }
                return number;
            }
        }
    }
}
