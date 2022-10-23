using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FuelAppAPI.Services;
using FuelAppAPI.Models;
using FuelAppAPI.DTO;
using FuelAppAPI.Converters;

/*
 * IT19014128
 * A.M.W.W.R.L. Wataketiya
 * 
 * This is the controller for QueueLogItems
 * 
 * Adding queue log items is done via FuelStationController when incrementing or decrementing the queue length
 * 
 */

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
        public async Task<ActionResult<QueueLogItemDto>> Get(string id)
        {
            var queueLogItem = await _queueLogService.GetAsync(id);

            //if no log exists for the given id
            //return not found status
            if(queueLogItem is null)
            {
                return NotFound();
            }

            QueueLogItem item = new QueueLogItem();
            item = queueLogItem;
            DateTime dateTime = item.dateTime ?? DateTime.Now; //using null coalsing operator

            QueueLogItemDto queueLogItemDto = new QueueLogItemDto();
            queueLogItemDto.Id = item.Id;
            queueLogItemDto.CustomerUsername = item.CustomerUsername;
            queueLogItemDto.StationId = item.StationId;
            queueLogItemDto.StationLicense = item.StationLicense;
            queueLogItemDto.StationName = item.StationName;
            queueLogItemDto.Queue = item.Queue;
            queueLogItemDto.Action = item.Action;
            queueLogItemDto.RefuelStatus = item.RefuelStatus;
            queueLogItemDto.dateTime = item.dateTime;

            //set the date and time details seperately
            queueLogItemDto.Year = dateTime.Year;
            queueLogItemDto.Month = dateTime.Month;
            queueLogItemDto.DayNumber = dateTime.Day;
            queueLogItemDto.Hour = dateTime.Hour;
            queueLogItemDto.Minute = dateTime.Minute;
            queueLogItemDto.Second = dateTime.Second;

            return queueLogItemDto;
        }

        //endpoint to get log items by the username
        [Route("[action]/{username}")]
        [HttpGet]
        public async Task<List<QueueLogItemDto>> GetQueueLogItemsByUsername(string username)
        {
            List<QueueLogItem> queueLogItems = await _queueLogService.GetByUsername(username); //get queue log items by username
            List<QueueLogItemDto> queueLogItemDtos = new List<QueueLogItemDto>();

            //go through all the queue log item model objects
            foreach (QueueLogItem queueLogItem in queueLogItems)
            {
                QueueLogItemDto queueLogItemDto = QueueLogDtoConverter.convertModelToDtoWithId(queueLogItem);//convert the model to a DTO
                queueLogItemDtos.Add(queueLogItemDto); //add the DTO to the DTO list
            }
            return queueLogItemDtos;
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

