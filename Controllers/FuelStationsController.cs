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
 * IT19014128
 * A.M.W.W.R.L. Wataketiya
 * Controller for the fuel stations
 * Endpoints are handled here
 */
namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelStationsController : ControllerBase
    {
        private readonly FuelStationService _fuelStationService; //fuel station service
        private readonly FuelStationArchiveService _fuelStationArchiveService; // fuel station archive service
        private readonly QueueLogService _queueLogService; //queue log service
        private readonly FuelStationLogService _fuelStationLogService; //fuel station log service

        //constructor
        public FuelStationsController(FuelStationService fuelStationService, FuelStationArchiveService fuelStationArchiveService,
            QueueLogService queueLogService, FuelStationLogService fuelStationLogService)
        {
            _fuelStationService = fuelStationService;
            _fuelStationArchiveService = fuelStationArchiveService;
            _queueLogService = queueLogService;
            _fuelStationLogService = fuelStationLogService;
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
            newFuelStation.PetrolQueueLength = receivedFuelStation.PetrolQueueLength;
            newFuelStation.DieselQueueLength = receivedFuelStation.DieselQueueLength;
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
        //the main entry is deleted
        //an archive entry is added in the database
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            FuelStation fuelStation = await _fuelStationService.GetAsync(id);

            if(fuelStation is null)
            {
                return NotFound();
            }

            
            //create fuel station archive object
            FuelStationArchive fuelStationArchive = new FuelStationArchive();

            //assign attributes to the archive object
            fuelStationArchive.License = fuelStation.License;
            fuelStationArchive.OwnerUsername = fuelStation.OwnerUsername;
            fuelStationArchive.StationName = fuelStation.StationName;
            fuelStationArchive.StationAddress = fuelStation.StationAddress;
            fuelStationArchive.StationPhoneNumber = fuelStation.StationPhoneNumber;
            fuelStationArchive.StationEmail = fuelStation.StationEmail;
            fuelStationArchive.StationWebsite = fuelStation.StationWebsite;

            // create archive entry
            await _fuelStationArchiveService.CreateAsync(fuelStationArchive);

            //delete fuel station entry from main collection
            await _fuelStationService.DeleteAsync(id);

            return NoContent();
        }

        //endpoint to get the station by the owner's username
        //GET api/GetStationByUsername/madura
        [Route("[action]/{ownerUsername}")]
        [HttpGet]
        public async Task<ActionResult<FuelStation>> GetStationByOwner(string ownerUsername)
        {
            var fuelStation = await _fuelStationService.GetStationByOwnerUsernameAsync(ownerUsername);

            //return not found of no fuel station is found for the username
            if(fuelStation is null)
            {
                return NotFound();
            }

            //return the fuel station
            return fuelStation;
        }

        //endpoint to get all the stations for a owner's username
        //GET api/GetAllStationsByOwnerUsername/madura
        [Route("[action]/{ownerUsername}")]
        [HttpGet]
        public async Task<ActionResult<List<FuelStationDto>>> GetAllStationsByOwnerUsername(string ownerUsername)
        {
            List<FuelStation> fuelStations = await _fuelStationService.GetAllStationsByOwnerUsernameAsync(ownerUsername); //retrieve all fuel stations for owner
            List<FuelStationDto> fuelStationDtos = new List<FuelStationDto>();

            //go through all the fuel station model objects
            foreach(FuelStation fuelStation in fuelStations)
            {
                FuelStationDto fuelStationDto = FuelStationDtoConverter.convertModelToDtoWithId(fuelStation);//convert the model to a DTO
                fuelStationDtos.Add(fuelStationDto); //add the DTO to the DTO list
            }

            return fuelStationDtos;
        }

        //endpoint to increase petrol queue length
        [Route("[action]/{id}")]
        [HttpPut]
        public async Task<ActionResult> IncrementPetrolQueueLength(string id, [FromBody] QueueLogRequestDto queueLogRequestDto)
        {
            var fuelStation = await _fuelStationService.GetAsync(id);

            //return not found of no fuel station is found for the username
            if (fuelStation is null)
            {
                return NotFound();
            }

            await _fuelStationService.IncrementPetrolQueueLength(id); //incrementCount in the fuel station

            //initialize a queue log item instance
            QueueLogItem queueLogItem = new QueueLogItem();

            //get the details from queueLogRequestDto and assign to queueLogItem
            queueLogItem.CustomerUsername = queueLogRequestDto.CustomerUsername;
            queueLogItem.StationId = queueLogRequestDto.StationId;
            queueLogItem.StationLicense = queueLogRequestDto.StationLicense;
            queueLogItem.StationName = queueLogRequestDto.StationName;
            queueLogItem.RefuelStatus = "not-applicable";

            //reassign the queue and the action
            queueLogItem.Queue = "petrol";
            queueLogItem.Action = "join";

            //set the current time to the log item's datetime
            DateTime currentDateTime = DateTime.Now;
            Console.WriteLine("Current Date Time : " + currentDateTime);
            queueLogItem.dateTime = currentDateTime;

            //create new log entry
            await _queueLogService.CreateAsync(queueLogItem);

            return NoContent();
        }

        //endpoint to decrase petrol queue length
        [Route("[action]/{id}")]
        [HttpPut]
        public async Task<ActionResult> DecrementPetrolQueueLength(string id, [FromBody] QueueLogRequestDto queueLogRequestDto)
        {
            FuelStation fuelStation = await _fuelStationService.GetAsync(id);

            //return not found of no fuel station is found for the username
            if (fuelStation is null)
            {
                return NotFound();
            }

            await _fuelStationService.DecrementPetrolQueueLength(id);

            //check if queue length is greater than zero
            if(fuelStation.PetrolQueueLength > 0)
            {
                //initialize a queue log item instance
                QueueLogItem queueLogItem = new QueueLogItem();

                //get the details from queueLogRequestDto and assign to queueLogItem
                queueLogItem.CustomerUsername = queueLogRequestDto.CustomerUsername;
                queueLogItem.StationId = queueLogRequestDto.StationId;
                queueLogItem.StationLicense = queueLogRequestDto.StationLicense;
                queueLogItem.StationName = queueLogRequestDto.StationName;
                queueLogItem.RefuelStatus = queueLogRequestDto.RefuelStatus;

                //reassign the queue and the action
                queueLogItem.Queue = "petrol";
                queueLogItem.Action = "leave";

                //set the current time to the log item's datetime
                DateTime currentDateTime = DateTime.Now;
                Console.WriteLine("Current Date Time : " + currentDateTime);
                queueLogItem.dateTime = currentDateTime;

                //create new log entry
                await _queueLogService.CreateAsync(queueLogItem);
            }

            

            return NoContent();
        }

        //endpoint to increment diesel queue length
        [Route("[action]/{id}")]
        [HttpPut]
        public async Task<ActionResult> IncrementDieselQueueLength(string id, [FromBody] QueueLogRequestDto queueLogRequestDto)
        {
            var fuelStation = await _fuelStationService.GetAsync(id);

            //return not found of no fuel station is found for the username
            if (fuelStation is null)
            {
                return NotFound();
            }

            await _fuelStationService.IncrementDieselQueueLength(id);

            //initialize a queue log item instance
            QueueLogItem queueLogItem = new QueueLogItem();

            //get the details from queueLogRequestDto and assign to queueLogItem
            queueLogItem.CustomerUsername = queueLogRequestDto.CustomerUsername;
            queueLogItem.StationId = queueLogRequestDto.StationId;
            queueLogItem.StationLicense = queueLogRequestDto.StationLicense;
            queueLogItem.StationName = queueLogRequestDto.StationName;
            queueLogItem.RefuelStatus = "not-appliable";

            //reassign the queue and the action
            queueLogItem.Queue = "diesel";
            queueLogItem.Action = "join";

            //set the current time to the log item's datetime
            DateTime currentDateTime = DateTime.Now;
            Console.WriteLine("Current Date Time : " + currentDateTime);
            queueLogItem.dateTime = currentDateTime;

            //create new log entry
            await _queueLogService.CreateAsync(queueLogItem);


            return NoContent();
        }

        //endpoint to decrement diesel queue length
        [Route("[action]/{id}")]
        [HttpPut]
        public async Task<ActionResult> DecrementDieselQueueLength(string id, [FromBody] QueueLogRequestDto queueLogRequestDto)
        {
            FuelStation fuelStation = await _fuelStationService.GetAsync(id);

            //return not found of no fuel station is found for the username
            if (fuelStation is null)
            {
                return NotFound();
            }

            await _fuelStationService.DecrementDieselQueueLength(id);

            if(fuelStation.DieselQueueLength > 0)
            {
                //initialize a queue log item instance
                QueueLogItem queueLogItem = new QueueLogItem();

                //get the details from queueLogRequestDto and assign to queueLogItem
                queueLogItem.CustomerUsername = queueLogRequestDto.CustomerUsername;
                queueLogItem.StationId = queueLogRequestDto.StationId;
                queueLogItem.StationLicense = queueLogRequestDto.StationLicense;
                queueLogItem.StationName = queueLogRequestDto.StationName;
                queueLogItem.RefuelStatus = queueLogRequestDto.RefuelStatus;

                //reassign the queue and the action
                queueLogItem.Queue = "diesel";
                queueLogItem.Action = "leave";

                //set the current time to the log item's datetime
                DateTime currentDateTime = DateTime.Now;
                Console.WriteLine("Current Date Time : " + currentDateTime);
                queueLogItem.dateTime = currentDateTime;

                //create new log entry
                await _queueLogService.CreateAsync(queueLogItem);
            }
            return NoContent();
        }

        //endpoint to mark as petrol avaialable
        [Route("[action]/{id}")]
        [HttpPut]
        public async Task<ActionResult> MarkPetrolAsAvailable(string id)
        {
            var fuelStation = await _fuelStationService.GetAsync(id);

            //return not found of no fuel station is found for the username
            if (fuelStation is null)
            {
                return NotFound();
            }

            await _fuelStationService.UpdatePetrolStatus(id, "available");

            //instantiate fuel station log item
            FuelStationLogItem fuelStationLogItem = new FuelStationLogItem();
            //add the data
            fuelStationLogItem.StationId = id;
            fuelStationLogItem.FuelType = "petrol";
            fuelStationLogItem.FuelStatus = "available";

            //get current date time
            DateTime currentDateTime = DateTime.Now;
            fuelStationLogItem.DateTime = currentDateTime; //set the current date time

            //create the log entry
            await _fuelStationLogService.CreateAsync(fuelStationLogItem);

            return NoContent();
        }

        //endpoint to mark as petrol unavaialable
        [Route("[action]/{id}")]
        [HttpPut]
        public async Task<ActionResult> MarkPetrolAsUnavailable(string id)
        {
            var fuelStation = await _fuelStationService.GetAsync(id);

            //return not found of no fuel station is found for the username
            if (fuelStation is null)
            {
                return NotFound();
            }

            await _fuelStationService.UpdatePetrolStatus(id, "unavailable");

            //instantiate fuel station log item
            FuelStationLogItem fuelStationLogItem = new FuelStationLogItem();
            //add the data
            fuelStationLogItem.StationId = id;
            fuelStationLogItem.FuelType = "petrol";
            fuelStationLogItem.FuelStatus = "unavailable";

            //get current date time
            DateTime currentDateTime = DateTime.Now;
            fuelStationLogItem.DateTime = currentDateTime; //set the current date time

            //create the log entry
            await _fuelStationLogService.CreateAsync(fuelStationLogItem);


            return NoContent();
        }

        //endpoint to mark as diesel avaialable
        [Route("[action]/{id}")]
        [HttpPut]
        public async Task<ActionResult> MarkDieselAsAvailable(string id)
        {
            var fuelStation = await _fuelStationService.GetAsync(id);

            //return not found of no fuel station is found for the username
            if (fuelStation is null)
            {
                return NotFound();
            }

            await _fuelStationService.UpdateDieselStatus(id, "available");

            //instantiate fuel station log item
            FuelStationLogItem fuelStationLogItem = new FuelStationLogItem();
            //add the data
            fuelStationLogItem.StationId = id;
            fuelStationLogItem.FuelType = "diesel";
            fuelStationLogItem.FuelStatus = "available";

            //get current date time
            DateTime currentDateTime = DateTime.Now;
            fuelStationLogItem.DateTime = currentDateTime; //set the current date time

            //create the log entry
            await _fuelStationLogService.CreateAsync(fuelStationLogItem);


            return NoContent();
        }

        //endpoint to mark as diesel unavaialable
        [Route("[action]/{id}")]
        [HttpPut]
        public async Task<ActionResult> MarkDieselAsUnavailable(string id)
        {
            var fuelStation = await _fuelStationService.GetAsync(id);

            //return not found of no fuel station is found for the username
            if (fuelStation is null)
            {
                return NotFound();
            }

            await _fuelStationService.UpdateDieselStatus(id, "unavailable");

            //instantiate fuel station log item
            FuelStationLogItem fuelStationLogItem = new FuelStationLogItem();
            //add the data
            fuelStationLogItem.StationId = id;
            fuelStationLogItem.FuelType = "diesel";
            fuelStationLogItem.FuelStatus = "unavailable";

            //get current date time
            DateTime currentDateTime = DateTime.Now;
            fuelStationLogItem.DateTime = currentDateTime; //set the current date time

            //create the log entry
            await _fuelStationLogService.CreateAsync(fuelStationLogItem);


            return NoContent();
        }

        //endpoint to mark as station open
        [Route("[action]/{id}")]
        [HttpPut]
        public async Task<ActionResult> MarkStationAsOpen(string id)
        {
            var fuelStation = await _fuelStationService.GetAsync(id);

            //return not found of no fuel station is found for the username
            if (fuelStation is null)
            {
                return NotFound();
            }

            await _fuelStationService.UpdateStationOpenStatus(id, "open");
            return NoContent();
        }

        //endpoint to mark as station closed
        [Route("[action]/{id}")]
        [HttpPut]
        public async Task<ActionResult> MarkStationAsClosed(string id)
        {
            var fuelStation = await _fuelStationService.GetAsync(id);

            //return not found of no fuel station is found for the username
            if (fuelStation is null)
            {
                return NotFound();
            }

            await _fuelStationService.UpdateStationOpenStatus(id, "closed");
            return NoContent();
        }

    }
}

