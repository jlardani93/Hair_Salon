using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using HairSalonProject.Models;

namespace HairSalonProject.Controllers
{
    public class Specialty_StylistController : Controller
    {
        [HttpPost("/Specialty_Stylist/Create/{specialtyId}/{stylistId}/{fromStylist}")]
        public ActionResult Create(int mySpecialtyId, int myStylistId, int fromStylist)
        {
            DB.AddSpecialty_Stylist(mySpecialtyId, myStylistId);
            if (fromStylist == 1)
            {
                return RedirectToAction("Info", "Stylist", new {stylistId = myStylistId});
            }
            return RedirectToAction("Info", "Specialty", new {specialtyId = mySpecialtyId});
        }
    }
}
