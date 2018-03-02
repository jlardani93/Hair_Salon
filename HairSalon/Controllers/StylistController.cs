using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using HairSalonProject.Models;

namespace HairSalonProject.Controllers
{
    public class StylistController : Controller
    {
        [HttpGet("/Stylist/Form")]
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost("/Stylist/Create")]
        public ActionResult Create()
        {
            string name = Request.Form["name"];
            Stylist myStylist = new Stylist(name);
            myStylist.Save();
            return View("Info", myStylist);
        }

        [HttpGet("/Stylist/Info/{id}")]
        public ActionResult Info(int id)
        {
            Stylist myStylist = Stylist.Find(id);
            return View(myStylist);
        }

        [HttpGet("/Stylist/Delete/{stylistId}")]
        public ActionResult Delete(int stylistId)
        {
            Stylist myStylist = Stylist.Find(id);
            myStylist.Delete();
            return RedirectToAction("/", "Home"); 
        }

        [HttpGet("/Stylist/DeleteAll")]
        public ActionResult DeleteAll()
        {
            Stylist.DeleteAll();
            return RedirectToAction("/", "Home");
        }

    }
}
