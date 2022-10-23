using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FuelAppAPI.Services;
using FuelAppAPI.Models;
using FuelAppAPI.DTO;

/*
 * IT19014128
 * A.M.W.W.R.L. Wataketiya
 * 
 * This is the controller for FuelStationLogItems
 * 
 * Adding queue log items is done via FuelStationController when incrementing or decrementing the queue length
 * 
 */

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelStationLogsController : ControllerBase
    {
        private readonly FuelStationLogService _fuelStationLogService; //fuel station log service

        //constructor
        public FuelStationLogsController(FuelStationLogService fuelStationLogService)
        {
            _fuelStationLogService = fuelStationLogService;
        }

        //endpoint to get all logs for all fuel stations
        // GET: api/values
        [HttpGet]
        public async Task<List<FuelStationLogItem>> Get()
        {
            var fuelStationLogItem = await _fuelStationLogService.GetAsync();
            return fuelStationLogItem;
        }

        //endpoint to get a log by id
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<FuelStationLogItem> Get(string id)
        {
            var fuelStationLogItem = await _fuelStationLogService.GetAsync(id);
            return fuelStationLogItem;
        }

        //endpoint to get logs by station id
        /*public async Task<List<FuelStationLogDto>> GetByStationId(string id)
        {
            List<FuelStationLogItem> fuelStationLogItems = new List<FuelStationLogItem>();
            fuelStationLogItems = _fuelStationLogService.Get
        }*/

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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

