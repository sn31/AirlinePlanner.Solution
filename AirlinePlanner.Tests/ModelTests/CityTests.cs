using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using AirlinePlanner.Models;

namespace AirlinePlanner.Tests
{
  [TestClass]
  public class CityTests : IDisposable
  {
    public void Dispose()
    {
      City.DeleteAll();
    }
    public CityTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=airline_planner_test;";
    }

    [TestMethod]
    public void GetAll_DBStartsEmpty_0()
    {
        //Arrange
        //Act
        int testCities = City.GetAll().Count;
        
        Assert.AreEqual(0, testCities);
    }
    [TestMethod]
    public void GetAll_DbCompareObjects_Equal()
    {
        //Arrange
        List <City> cities = new List<City>{};
        City newCity = new City("Seattle");
        newCity.Save();

        //Act
        cities = City.GetAll();

        //Assert
        Assert.AreEqual(newCity, cities[0]);

    }
    [TestMethod]
    public void Find_FindCityDatabase_City()
    {
        City newCity = new City("Seattle");
        newCity.Save();

        City testCity = City.Find(newCity.Id);

        Assert.AreEqual(newCity,testCity);
    }

    [TestMethod]
    public void Edit_UpdatesItemInDB()
    {
        City newCity = new City("Seattle");
        newCity.Save();
        string testCity = "San Jose";
        newCity.Edit(testCity);

        Assert.AreEqual(testCity, newCity.Name);
    }

    [TestMethod]
    public void DeleteCity_DeleteCityFromDB()
    {
        City newCity = new City("Seatlle");
        newCity.Save();

        City.DeleteCity(newCity.Id);
        int count = City.GetAll().Count;

        Assert.AreEqual(0, count);
    }


  }
}