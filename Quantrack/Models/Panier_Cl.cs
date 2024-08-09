﻿using MySql.Data.MySqlClient;

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
                var cm = new MySqlCommand("AddToCart", cnx);
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
    }
}
