using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalonProject.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalonProject.Tests
{
    public class ClientTest : IDisposable
    {
        public void Dispose()
        {
            Client.DeleteAll();
            Stylist.DeleteAll();
        }

        public ClientTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=justin_lardani_test;";
        }

        [TestMethod]
        public void Find_FindClientInDatabase_Client()
        {
            Client testClient = new Client("Justin", 1);
            testClient.Save();

            Client foundClient = Client.Find(testClient.GetId());

            Assert.AreEqual(testClient, foundClient);
        }
    }
}
