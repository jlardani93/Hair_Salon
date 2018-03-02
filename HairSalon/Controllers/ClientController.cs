using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using HairSalonProject.Models;

namespace HairSalonProject.Controllers
{
    public class ClientController : Controller
    {
        [HttpGet("/Client/Form/{stylistId}")]
        public ActionResult Form(int stylistId)
        {
            Stylist myStylist = Stylist.Find(stylistId);
            return View(myStylist);
        }

        [HttpPost("/Client/Create")]
        public ActionResult Create()
        {
            string name = Request.Form["name"];
            int stylistId = Int32.Parse(Request.Form["stylistId"]);
            Client myClient = new Client(name, stylistId);
            myClient.Save();
            return RedirectToAction("Info", "Stylist", new {id=stylistId});
        }

        [HttpGet("/Client/Info/{clientId}")]
        public ActionResult Info(int clientId)
        {
            Client myClient = Client.Find(clientId);
            Stylist myStylist = Stylist.Find(myClient.GetStylistId());
            ViewBag.stylist = myStylist.GetName();
            ViewBag.stylists = Stylist.GetAll();
            return View(myClient);
        }

        [HttpPost("/Client/Update/{clientId}")]
        public ActionResult Update(int clientId)
        {
            string name = Request.Form["name"];
            int stylistId = Int32.Parse(Request.Form["stylistId"]);
            Client myClient = Client.Find(clientId);
            myClient.Update(name, stylistId);
            return RedirectToAction("Info", new {clientId = clientId});
        }

        [HttpGet("/Client/Delete/{clientId}/{stylistId}")]
        public ActionResult Delete(int clientId, int myStylistId)
        {
            Client myClient = Client.Find(clientId);
            myClient.Delete();
            return RedirectToAction("Info", "Stylist", new {stylistId=myStylistId});
        }
    }
}
