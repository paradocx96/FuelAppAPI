using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelAppAPI.DTO;
using FuelAppAPI.Models;
using FuelAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

/*
IT19014128
A.M.W.W.R.L. Wataketiya
 */
namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelStationsController : ControllerBase
    {
        private readonly FuelStationService _fuelStationService; //fuel station service 

        //constructor
        public FuelStationsController(FuelStationService fuelStationService)
        {
            _fuelStationService = fuelStationService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<List<FuelStation>> Get()
        {
            var fuelStations = await _fuelStationService.GetAsync();
            return fuelStations;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FuelStation>> Get(string id)
        {
            var fuelStation = await _fuelStationService.GetAsync(id);

            //if no fuel station exists for the given id
            // return not found status
            if(fuelStation is null)
            {
                return NotFound();
            }

            return fuelStation; //return the fuel station
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FuelStationDto receivedFuelStation)
        {
            FuelStation newFuelStation = new FuelStation();  //instantiate new fuel station instance
            // populate the fuel station instance

            // populate fuel station data
            newFuelStation.License = receivedFuelStation.License;
            newFuelStation.OwnerUsername = receivedFuelStation.OwnerUsername;
            newFuelStation.StationName = receivedFuelStation.StationName;
            newFuelStation.StationAddress = receivedFuelStation.StationAddress;
            newFuelStation.StationPhoneNumber = receivedFuelStation.StationPhoneNumber;
            newFuelStation.StationEmail = receivedFuelStation.StationEmail;
            newFuelStation.StationWebsite = receivedFuelStation.StationWebsite;

            // populate fuel station status data with initial data
            // initial status data may be dummy data
            newFuelStation.OpenStatus = receivedFuelStation.OpenStatus;
            newFuelStation.QueueLength = receivedFuelStation.QueueLength;
            newFuelStation.PetrolStatus = receivedFuelStation.PetrolStatus;
            newFuelStation.DieselStatus = receivedFuelStation.DieselStatus;

            //populate the fuel station location details
            // location implementation might no be complete
            // location data is stored in the database
            newFuelStation.LocationLatitude = receivedFuelStation.LocationLatitude;
            newFuelStation.LocationLongitude = receivedFuelStation.LocationLongitude;

            //create the db entry
            await _fuelStationService.CreateAsync(newFuelStation);

            return CreatedAtAction(nameof(Get), new { Id = newFuelStation.Id }, newFuelStation);


        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

