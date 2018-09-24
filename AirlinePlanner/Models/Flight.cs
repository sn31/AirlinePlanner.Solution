using System;
using System.Collections.Generic;
using AirlinePlanner;
using MySql.Data.MySqlClient;

namespace AirlinePlanner.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime DepartureTime { get; set; }

        public string Status { get; set; }

        public Flight (string newName, DateTime newDepartureTime, string newStatus, int id = 0)
        {
            Name = newName;
            DepartureTime = newDepartureTime;
            Status = newStatus;
            Id = id;
        }

        public void Save ()
        {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = @"INSERT INTO flights WHERE (name, departure_time, status) VALUES (@newName, @newDepartureTime,@newStatus);";
            cmd.Parameters.AddWithValue("@newName",this.Name);
            cmd.Parameters.AddWithValue("@newDepartureTime",this.DepartureTime);
            cmd.Parameters.AddWithValue("@newStatus",this.Status);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}