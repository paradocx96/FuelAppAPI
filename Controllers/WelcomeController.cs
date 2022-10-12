﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace FuelAppAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class WelcomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Welcome()
        {
            dynamic data = new ExpandoObject();
            data.message = "Fuel APP API Server Running!";
            data.active = true;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            return Ok(json);
        }
    }
}
