using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace HairSalonProject.Models
{
    public class Specialty
    {
        private int _id;
        private string _name;

        public void Specialty(string name)
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
            cmd.CommandText = @"INSERT INTO specialties (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter("@name", _name);
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Dispose();
        }

        public void AddStylist(int newStylistId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialties_stylists (specialty_id, stylist_id) VALUES (@specialtyId, @stylistId);";

            MySqlParameter specialtyId = new MySqlParameter("@specialtyId", _id);
            MySqlParameter stylistId = new MySqlParameter("@stylistId", newStylistId);
            cmd.Parameters.Add(specialtyId);
            cmd.Parameters.Add(stylistId);

            cmd.ExecuteNonQuery();

            conn.Dispose();
        }

        public List<Specialty> GetAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Specialty> mySpecialties = new List<Specialty>();

            while (rdr.Read())
            {
                int id = (int) rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Specialty newSpecialty = new Specialty(name);
                newSpecialty.SetId(id);
                mySpecialties.Add(newSpecialty);
            }

            conn.Dispose();

            return mySpecialties;
        }

        public List<Stylist> GetStylists()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM specialties JOIN specialties_stylists ON (specialties.id = specialties_stylists.specialty_id) JOIN stylists ON (specialties_stylists.stylist_id = stylists.id) WHERE specialties.id = @specialtyId;";

            MySqlParameter specialtyId = new MySqlParamter("@specialtyId", _id);
            cmd.Parameters.Add(specialtyId);

            List<Stylist> myStylists = new List<Stylist>();

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int id = (int) rdr.GetInt32(0);
                string name = rdr.GetName(1);
                Stylist myStylist = new Stylist(name);
                myStylist.SetId(id);
                myStylists.Add(myStylist);
            }

            conn.Dispose();

            return myStylists;
        }

        public Specialty Find(int specialtyId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties WHERE id = @id;";

            MySqlParameter id = new MySqlParameter("@id", specialtyId);
            cmd.Parameters.Add(id);

            int myId = 0;
            string myName = "";

            MySqlDataReader = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                myId = (int) rdr.GetInt32(0);
                myName = rdr.GetString(1);
            }

            Specialty mySpecialty = new Specialty(myName);
            mySpecialty.SetId(myId);

            conn.Dispose();

            return mySpecialty;
        }

        public Specialty Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialties_stylists WHERE specialty_id = @id;
            DELETE FROM specialties WHERE id = @id;";

            MySqlParameter id = newMySqlParameter("@id", _id);
            cmd.Parameters.Add(id);

            cmd.ExecuteNonQuery();

            conn.Dispose(); 
        }
    }
}
