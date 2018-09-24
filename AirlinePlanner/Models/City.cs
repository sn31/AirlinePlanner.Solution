using System;
using System.Collections.Generic;
using AirlinePlanner;
using MySql.Data.MySqlClient;

namespace AirlinePlanner.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public City(string newName, int id = 0)
        {
            Name = newName;
            Id = id;
        }

        public override bool Equals(System.Object otherCity)
        {
            if (!(otherCity is City))
            {
                return false;
            }
            else
            {
                City newCity = (City) otherCity;
                bool idEquality = (this.Id == newCity.Id);
                bool nameEquality = (this.Name == newCity.Name);
                return (nameEquality && idEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO cities (name) VALUES (@name);";

            cmd.Parameters.AddWithValue("@name", this.Name);

            cmd.ExecuteNonQuery();
            Id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<City> GetAll()
        {
            List<City> allCities = new List<City> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM cities;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                City newCity = new City(name, id);
                allCities.Add(newCity);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCities;
        }

        public static City Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM cities WHERE id=@thisId;";

            cmd.Parameters.AddWithValue("@thisId", id);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            int cityId = 0;
            string cityName = "";

            while (rdr.Read())
            {
                cityId = rdr.GetInt32(0);
                cityName = rdr.GetString(1);
            }

            City foundCity = new City(cityName, cityId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundCity;

        }

        public void Edit(string newCity)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE cities SET name = @newCity WHERE id = @searchId;";

            cmd.Parameters.AddWithValue("@searchId", Id);
            cmd.Parameters.AddWithValue("@newCity", newCity);

            this.Name = newCity;
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteCity(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM cities WHERE id = @searchId;";

            cmd.Parameters.AddWithValue("@searchId", id);

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

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM cities;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }

}