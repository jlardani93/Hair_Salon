using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace HairSalonProject.Models
{
    public class Client
    {
        private int _id;
        private string _name;
        private int _stylistId;

        public Client(string name, int stylistId)
        {
            _name = name;
            _stylistId = stylistId;
        }

        public void SetId(int id()
        {
            _id = id;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetStylistId()
        {
            return _stylistId;
        }

        public void Save()
        {
           MySqlConnection conn = DB.Connection();
           conn.Open();
           MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@name, @stylistId);";

           MySqlParameter name = new MySqlParameter("@name", _name);
           MySqlParameter stylistId = new MySqlParameter("@stylistId", _stylistId);
           cmd.Parameters.Add(name);
           cmd. Parameters.Add(stylistId);

           cmd.ExecuteNonQuery();
           _id = (int) cmd.LastInsertedId;

           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
        }

        public static Client Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id=@id;";

            MySqlParameter stylistId = new MySqlParameter();
            id.ParameterName = "@id";
            id.Value = id;
            cmd.Parameters.Add(stylistId);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            string tempName = "";
            int tempStylistId = 0;

            while (rdr.Read())
            {
                tempName = rdr.GetString(1);
                tempStylistId = (int) rdr.GetInt32(2);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            Client myClient = new Client(tempName, tempStylistId);
            myClient.SetId(id);
            return myClient;
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients WHERE id=@id;";

            MySqlParameter id = new MySqlParameter();
            id.ParameterName = "@id";
            id.Value = _id;
            cmd.Parameters.Add(id);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool nameEquality = (_name == newClient.GetName());
                return nameEquality;
            }
        }
    }
}
