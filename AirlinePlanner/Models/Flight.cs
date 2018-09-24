using System;
using System.Collections.Generic;
using AirlinePlanner;
using MySql.Data.MySqlClient;

namespace AirlinePlanner.Models
{
    public class Flight
    {
        public int Id {get;set;}
        public string Name {get;set;}

        public DateTime DepartureTime {get;set;}

        public string Status {get;set;}
        
    }
}