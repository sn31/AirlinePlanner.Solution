using System;
using System.Collections.Generic;
using AirlinePlanner;
using MySql.Data.MySqlClient;

namespace AirlinePlanner.Models {
    public class City {
        public int Id { get; set; }
        public string Name { get; set; }

        public City (string newName, int id = 0) {
            Name = newName;
            Id = id;
        }

        public void Save () {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();

            MySqlCommand cmd = conn.CreateCommand ();
            cmd.CommandText = @"INSERT INTO cities (name) VALUES (@name);";

            cmd.Parameters.AddWithValue ("@name", this.Name);

            cmd.ExecuteNonQuery ();
            Id = (int) cmd.LastInsertedId;

            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }

        }
    }

}