/*
 * EAD - FuelMe APP API
 *
 * API Controller for API Root
 * 
 * @author IT19180526 - S.A.N.L.D. Chandrasiri
 * @version 1.0
 */

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