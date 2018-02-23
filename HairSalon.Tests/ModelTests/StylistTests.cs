using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalonProject.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalonProject.Tests
{
    [TestClass]
    public class StylistTest : IDisposable
    {
        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
        }

        public StylistTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=justin_lardani_test;";
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
            //Arrange, Act
            int result = Stylist.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesToDatabase_StylistList()
        {
            Stylist testStylist = new Stylist("Justin");

            testStylist.Save();
            List<Stylist> testStylists = Stylist.GetAll();
            List<Stylist> result = new List<Stylist>(){testStylist};

            CollectionAssert.AreEqual(result, testStylists);
        }

        [TestMethod]
        public void Find_FindStylistInDatabase_Stylist()
        {
            Stylist testStylist = new Stylist("Justin");
            testStylist.Save();

            Stylist foundStylist = Stylist.Find(testStylist.GetId());

            Assert.AreEqual(testStylist, foundStylist);
        }

    }
}
