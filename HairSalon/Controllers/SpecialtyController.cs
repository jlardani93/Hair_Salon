using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using HairSalonProject.Models;

namespace HairSalonProject.Controllers
{
    public class SpecialtyController : Controller
    {
        [HttpGet("/Specialty/Display")]
        public ActionResult Display()
        {
            List<Specialty> mySpecialties = Specialty.GetAll();
            return View();
        }

        [HttpGet("/Specialty/Form")]
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost("/Specialty/Create")]
        public ActionResult Create()
        {
            Specialty mySpecialty = new Specialty(Request.Form["name"]);
            mySpecialty.Save();

            return RedirectToAction("Info", new {specialtyId = mySpecialty.GetId()});
        }

        [HttpGet("/Specialty/Info/{specialtyId}")]
        public ActionResult Info(int specialtyId)
        {
            Specialty mySpecialty = Specialty.Find(specialtyId);
            ViewBag.stylists = Stylist.GetAll();
            return View(mySpecialty);
        }

        [HttpGet("/Specialty/Delete/{specialtyId}")]
        public ActionResult Delete(int specialtyId)
        {
            Specialty mySpecialty = Specialty.Find(specialtyId);
            mySpecialty.Delete();
            return RedirectToAction("Display");
        }

        [HttpGet("/Specialty/DeleteAll")]
        public ActionResult DeleteAll()
        {
            Specialty.DeleteAll();
            return RedirectToAction("Display");
        }
    }
}
