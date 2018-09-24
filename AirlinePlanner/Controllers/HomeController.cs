using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace AirlinePlanner.Controllers
{
    public class HomeController:Controller{
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
