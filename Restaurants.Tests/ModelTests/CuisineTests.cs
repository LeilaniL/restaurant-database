using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurants.Models;
using System.Collections.Generic;
using System;

namespace Restaurants.Tests
{
    [TestClass]
    public class CuisineTest
    {
        public CuisineTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=restaurants;";
        }
        [TestMethod]
        public void CuisineConstructor_CreatesCuisineObject_Cuisine()
        {
            Cuisine newCuisine = new Cuisine("Chinese");
            Assert.AreEqual(typeof(Cuisine), newCuisine.GetType());
        }
    }

}
