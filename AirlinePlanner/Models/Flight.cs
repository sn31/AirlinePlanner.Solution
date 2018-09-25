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

        public Flight(string newName, DateTime newDepartureTime, string newStatus, int id = 0)
        {
            Name = newName;
            DepartureTime = newDepartureTime;
            Status = newStatus;
            Id = id;
        }
        public override bool Equals(System.Object otherFlight)
        {
            if (!(otherFlight is Flight))
            {
                return false;
            }
            else
            {
                Flight newFlight = (Flight) otherFlight;
                bool idEquality = (this.Id == newFlight.Id);
                bool nameEquality = (this.Name == newFlight.Name);
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
            cmd.CommandText = @"INSERT INTO flights (name, departure_time, status) VALUES (@newName, @newDepartureTime,@newStatus);";
            cmd.Parameters.AddWithValue("@newName", this.Name);
            cmd.Parameters.AddWithValue("@newDepartureTime", this.DepartureTime);
            cmd.Parameters.AddWithValue("@newStatus", this.Status);

            cmd.ExecuteNonQuery();
            Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Flight Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM flights WHERE id=@thisId;";

            cmd.Parameters.AddWithValue("@thisId", id);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            int flightId = 0;
            string flightName = "";
            DateTime flightDateTime = new DateTime(1111, 11, 11);
            string flightStatus = "";

            while (rdr.Read())
            {
                flightId = rdr.GetInt32(0);
                flightName = rdr.GetString(1);
                flightDateTime = rdr.GetDateTime(2);
                flightStatus = rdr.GetString(3);

            }

            Flight foundFlight = new Flight(flightName,flightDateTime, flightStatus, flightId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundFlight;
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM flights;";

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Flight> GetAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM flights;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Flight> allFlights = new List<Flight> {};
            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string Name = rdr.GetString(1);
                DateTime DepartureTime = rdr.GetDateTime(2);
                string Status = rdr.GetString(3);
                Flight newFlight = new Flight(Name, DepartureTime, Status, Id);
                allFlights.Add(newFlight);
            }
            
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allFlights;
        }
    }
}