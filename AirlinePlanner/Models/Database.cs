using System;
using MySql.Data.MySqlClient;
using AirlinePlanner;
using static AirlinePlanner.Startup;

namespace AirlinePlanner.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
