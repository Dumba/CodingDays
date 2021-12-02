using System;
using CodingDays.Database;
using CodingDays.Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CodingDays.Controllers
{
    public class RegisterController : ControllerBase
    {
        public RegisterController(DB db)
        {
            _db = db;
        }

        private readonly DB _db;

        [HttpPost]
        public ActionResult Register(Models.Registration registration)
        {
            try
            {
                var reg = new Registration(registration);

                _db.Add(reg);
                _db.SaveChanges();

                return Redirect("/done.html");
            }
            catch (Exception)
            {
                return Redirect("/error.html");
            }
        }
    }
}