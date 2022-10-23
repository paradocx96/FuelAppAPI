using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FuelAppAPI.Services;
using FuelAppAPI.Models;
using FuelAppAPI.DTO;
using FuelAppAPI.Converters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueLogsController : ControllerBase
    {
        private readonly QueueLogService _queueLogService; //queue log service

        //constructor
        public QueueLogsController(QueueLogService queueLogService)
        {
            _queueLogService = queueLogService;
        }

        //endpoint to get all logs
        // GET: api/values
        [HttpGet]
        public async Task<List<QueueLogItem>>  Get()
        {
            var queueLogs = await _queueLogService.GetAsync();
            
            return queueLogs;
        }

        //endpoint to get a log item by id
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QueueLogItem>> Get(string id)
        {
            var queueLogItem = await _queueLogService.GetAsync(id);

            //if no log exists for the given id
            //return not found status
            if(queueLogItem is null)
            {
                return NotFound();
            }
            return queueLogItem;
        }

        //endpoint to get log items by the username
        [Route("[action]/{username}")]
        [HttpGet]
        public async Task<List<QueueLogItem>> GetQueueLogItemsByUsername(string username)
        {
            var queueLogItems = await _queueLogService.GetByUsername(username); //get queue log items by username
            return queueLogItems;
        }

        //endpoint to get log items by the station id
        [Route("[action]/{stationId}")]
        [HttpGet]
        public async Task<List<QueueLogItem>> GetQueueLogItemsByStationId(string stationId)
        {
            var queueLogItems = await _queueLogService.GetByStationId(stationId); //get queue log items by username
            return queueLogItems;
        }

        //adding queue log items is done via FuelStationController
        //Editing or deleting queue logs is not allowes and endpoints are not provided
    }
}

