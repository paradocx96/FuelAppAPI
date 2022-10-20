﻿using FuelAppAPI.Models;
using FuelAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {

        private readonly FavouriteService _favouriteService;

        public FavouriteController(FavouriteService newfavouriteService) => _favouriteService = newfavouriteService;

        // Get favourite by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Favourite>> GetFavouriteById(string id)
        {
            var favouriteObj = await _favouriteService.GetFavouriteByIdAsync(id);

            if (favouriteObj is null)
            {
                return NotFound();
            }

            return favouriteObj;
        }

       
    }
}
