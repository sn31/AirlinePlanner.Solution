using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using AirlinePlanner.Models;

namespace AirlinePlanner.Tests
{
  [TestClass]
  public class FlightTests : IDisposable
  {
    public void Dispose()
    {
      Flight.DeleteAll();
      City.DeleteAll();
    }
    public FlightTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=airline_planner_test;";
    }

    [TestMethod]
    public void GetAll_DBStartsEmpty_0()
    {
        //Arrange
        //Act
        int testFlight = Flight.GetAll().Count;
        
        Assert.AreEqual(0, testFlight);
    }
    // [TestMethod]
    // public void GetAll_DbCompareObjects_Equal()
    // {
    //     //Arrange
    //     List <Flight> cities = new List<Flight>{};
    //     Flight newFlight = new Flight("Seattle");
    //     newFlight.Save();

    //     //Act
    //     cities = Flight.GetAll();

    //     //Assert
    //     Assert.AreEqual(newFlight, cities[0]);

    // }
    // [TestMethod]
    // public void Find_FindFlightDatabase_Flight()
    // {
    //     Flight newFlight = new Flight("Seattle");
    //     newFlight.Save();

    //     Flight testFlight = Flight.Find(newFlight.Id);

    //     Assert.AreEqual(newFlight,testFlight);
    // }

    // [TestMethod]
    // public void Edit_UpdatesItemInDB()
    // {
    //     Flight newFlight = new Flight("Seattle");
    //     newFlight.Save();
    //     string testFlight = "San Jose";
    //     newFlight.Edit(testFlight);

    //     Assert.AreEqual(testFlight, newFlight.Name);
    // }

    // [TestMethod]
    // public void DeleteFlight_DeleteFlightFromDB()
    // {
    //     Flight newFlight = new Flight("Seatlle");
    //     newFlight.Save();

    //     Flight.DeleteFlight(newFlight.Id);
    //     int count = Flight.GetAll().Count;

    //     Assert.AreEqual(0, count);
    // }


  }
}