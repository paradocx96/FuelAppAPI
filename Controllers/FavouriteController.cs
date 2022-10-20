using FuelAppAPI.Models;
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
        [HttpGet("GetFavouriteById/{id}")]
        public async Task<ActionResult<Favourite>> GetFavouriteById(string id)
        {
            var favouriteObj = await _favouriteService.GetFavouriteByIdAsync(id);

            if (favouriteObj is null)
            {
                return NotFound();
            }

            return favouriteObj;
        }

        // Create new Favourite
        [HttpPost]
        public async Task<IActionResult> CreateFavourite(Favourite newfavourite)
        {
            await _favouriteService.CreateFavouriteAsync(newfavourite);

            return CreatedAtAction(nameof(GetFavouriteById), new { id = newfavourite.Id }, newfavourite);
        }

        // Get favourite by username
        [HttpGet("GetFavouriteByUsername/{username}")]
        public async Task<ActionResult<Favourite>> GetFavouriteByUsername(string username) { 
        
            var favouriteObj  = await _favouriteService.GetFavouriteByUsernameAsync(username);

            if(favouriteObj is null)
            {
                return NoContent();
            }

            return favouriteObj;
        }

        // Get all favorites
        [HttpGet]
        public async Task<List<Favourite>> GetAllFavourites() => await _favouriteService.GetAllFavouriteAsync();

       

    }
}
