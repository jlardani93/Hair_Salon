using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace HairSalonProject.Models
{
    public class Stylist
    {
        private int _id;
        private string _name;
        private List<Client> _clients = new List<Client>();

        public Stylist(string name)
        {
            _name = name;
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

        public void Save()
        {
           MySqlConnection conn = DB.Connection();
           conn.Open();
           MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@name);";

           MySqlParameter name = new MySqlParameter();
           name.ParameterName = "@name";
           name.Value = _name;
           cmd.Parameters.Add(name);

           cmd.ExecuteNonQuery();
           _id = (int) cmd.LastInsertedId;

           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
        }

        public static List<Stylist> GetAll()
        {
           List<Stylist> stylists = new List<Stylist>();

           MySqlConnection conn = DB.Connection();
           conn.Open();
           MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM stylists;";

           MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

           while (rdr.Read())
           {
               int id = (int) rdr.GetInt32(0);
               string name = rdr.GetString(1);
               Stylist myStylist = new Stylist(name);
               myStylist.SetId(id);
               stylists.Add(myStylist);
           }

           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }

           return stylists;
       }

        public List<Client> GetClients()
        {
            List<Client> myClients = new List<Client>();

            MySqlConnection conn = DB.Connection();
            conn.Open();


            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"Select * FROM clients WHERE stylist_id=@id;";

            MySqlParameter id = new MySqlParameter("@id", _id);
            cmd.Parameters.Add(id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                Client myClient = new Client(rdr.GetString(1), _id);
                myClient.SetId(rdr.GetInt32(0));
                myClients.Add(myClient);
            }

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return myClients;
        }

       public static Stylist Find(int id)
       {
           MySqlConnection conn = DB.Connection();
           conn.Open();
           MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM stylists WHERE id=@id;";

           MySqlParameter stylistId = new MySqlParameter("@id", id);
           cmd.Parameters.Add(stylistId);

           MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

           string tempName = "";
           int tempId = 0;

           while (rdr.Read())
           {
               tempId = rdr.GetInt32(0);
               tempName = rdr.GetString(1);
           }

           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
           Stylist myStylist = new Stylist(tempName);
           myStylist.SetId(tempId);
           return myStylist;
       }

        public void Update(string stylistName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE stylists SET name = @name WHERE id = @id;";

            MySqlParameter name = new MySqlParameter("@name", stylistName);
            MySqlParameter id = new MySqlParameter("@id", _id);
            cmd.Parameters.Add(name);
            cmd.Parameters.Add(id);

            cmd.ExecuteNonQuery();
            _name = stylistName;
            conn.Dispose(); 
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients WHERE stylist_id=@id;
            DELETE FROM stylists WHERE id=@id;";

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
            cmd.CommandText = @"DELETE FROM clients;
            DELETE FROM stylists;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public override bool Equals(System.Object otherStylist)
        {
            if (!(otherStylist is Stylist))
            {
                return false;
            }
            else
            {
                Stylist newStylist = (Stylist) otherStylist;
                bool nameEquality = (_name == newStylist.GetName());
                return nameEquality;
            }
        }

    }
}
