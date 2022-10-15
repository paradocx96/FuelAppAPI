using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelAppAPI.Converters;
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

        //endpoint to get all stations
        // GET: api/values
        [HttpGet]
        public async Task<List<FuelStation>> Get()
        {
            var fuelStations = await _fuelStationService.GetAsync();
            return fuelStations;
        }

        //endpoint to get a station by id
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

        //endpoint to create a station entry
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

        //endpoint for updating details of a station
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] FuelStationDto fuelStationToUpdateDTO)
        {
            var fuelStation = await _fuelStationService.GetAsync(id); //get the existing fuel station

            if(fuelStation is null)
            {
                return NotFound();
            }

            fuelStationToUpdateDTO.Id = id; //set the ID of the DTO
            FuelStation fuelStationToUpdate = new FuelStation();
            fuelStationToUpdate =  FuelStationDtoConverter.convertDtoToModelWithId(fuelStationToUpdateDTO); //convert the DTO to model
            await _fuelStationService.UpdateAsync(id, fuelStationToUpdate);

            return NoContent();

        }

        //endpoint to delete the station
        //consider archiving
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var fuelStation = _fuelStationService.GetAsync(id);

            if(fuelStation is null)
            {
                return NotFound();
            }

            await _fuelStationService.DeleteAsync(id);
            return NoContent();
        }

        //endpoint to get the station by the owner's username
        //GET api/GetStationByUsername/madura
        [Route("[action]/{username}")]
        [HttpGet]
        public void GetStationByOwner(string ownerUsername)
        {

        }

        //endpoint to increase queue count

        //endpoint to decrase queue count

        //endpoint to mark as petrol avaialable

        //endpoint to mark as petrol unavaialable

        //endpoint to mark as diesel avaialable

        //endpoint to mark as diesel unavaialable

        //endpoint to mark as station open

        //endpoint to mark as station closed


    }
}

