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
 * This is the controller for FuelStationLogItems
 * 
 * Adding station log items is done via FuelStationController when marking fuel availability
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
        [Route("[action]/{stationId}")]
        [HttpGet]
        public async Task<List<FuelStationLogDto>> GetByStationId(string stationId)
        {
            List<FuelStationLogItem> fuelStationLogItems = await _fuelStationLogService.GetByStationId(stationId);
            List<FuelStationLogDto> fuelStationLogDtos = new List<FuelStationLogDto>();
            fuelStationLogItems = await _fuelStationLogService.GetByStationId(stationId);

            //go through fuel station log items
            foreach(FuelStationLogItem fuelStationLogItem in fuelStationLogItems)
            {
                FuelStationLogDto fuelStationLogDto = FuelStationLogDtoConverter.convertModelToDtoWithId(fuelStationLogItem);
                fuelStationLogDtos.Add(fuelStationLogDto);
            }

            return fuelStationLogDtos;
        }

       
    }
}

