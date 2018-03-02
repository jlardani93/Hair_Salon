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

        public void SetId(int id)
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

        public static List<Client> GetAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";

            List<Client> myClients = new List<Client>();

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int id = (int) rdr.GetInt32(0);
                string tempName = rdr.GetString(1);
                int tempStylistId = (int) rdr.GetInt32(2);

                Client newClient = new Client(tempName, tempStylistId);
                newClient.SetId(id);
                myClients.Add(newClient);
            }

            conn.Dispose();

            return myClients;
        }

        public static Client Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id=@id;";

            MySqlParameter stylistId = new MySqlParameter("@id", id);
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

        public void Update(string clientName, int clientStylistId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE clients SET name = @name, stylist_id = @stylistId WHERE id = @id;";

            MySqlParameter name = new MySqlParameter("@name", clientName);
            MySqlParameter stylistId = new MySqlParameter("@stylistId", clientStylistId);
            MySqlParameter id = new MySqlParameter("@id", _id);
            cmd.Parameters.Add(name);
            cmd.Parameters.Add(stylistId);
            cmd.Parameters.Add(id);

            cmd.ExecuteNonQuery();
            _name = clientName;
            _stylistId = clientStylistId;

            conn.Dispose();
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
