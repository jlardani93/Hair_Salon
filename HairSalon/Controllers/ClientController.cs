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
            int stylistId = Request.Form["stylistId"];
            Client myClient = new Client(name, stylistId);
            myClient.Save();
            return RedirectToAction("Info", "Stylist", new {id=stylistId}); 
        }
    }
}
