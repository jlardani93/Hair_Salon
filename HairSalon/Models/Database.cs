using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace HairSalonProject.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }

        public static void AddSpecialty_Stylist(int newSpecialtyId, int newStylistId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialties_stylists (specialty_id, stylist_id) VALUES (@specialtyId, @stylistId);";

            MySqlParameter specialtyId = new MySqlParameter("@specialtyId", newSpecialtyId);
            MySqlParameter stylistId = new MySqlParameter("@stylistId", newStylistId);
            cmd.Parameters.Add(specialtyId);
            cmd.Parameters.Add(stylistId);

            cmd.ExecuteNonQuery();

            conn.Dispose();
        }
    }
}
