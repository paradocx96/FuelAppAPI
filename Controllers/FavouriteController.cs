/**
 * EAD - FuelMe API
 * 
 * @author H.G. Malwatta - IT19240848
 * 
 * @references
 * - https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-6.0&tabs=visual-studio
 * - https://mongodb.github.io/mongo-csharp-driver/2.18/getting_started/quick_tour/
 * - https://mongodb.github.io/mongo-csharp-driver/
 */

using FuelAppAPI.Models;
using FuelAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/**
 * H.G. Malwatta - IT19240848
 * 
 * This controller class is used to manipulate all favourite API related calls
 * 
 */

namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {

        private readonly FavouriteService _favouriteService;
        private readonly FuelStationService _fuelStationService;

        /**
        * Overloaded constructor
        * 
        * @param newfavouriteService
        * @param newfuelStationService
        * 
        */
        public FavouriteController(FavouriteService newfavouriteService, FuelStationService newfuelStationService)
        {
            _favouriteService = newfavouriteService;
            _fuelStationService = newfuelStationService;
        }

        /**
         * Get favourite by id
         * GET: api/Favourite
         * 
         * @param id
         * @return Task<ActionResult<Favourite>>
         */
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<Favourite>> GetFavouriteById(string id)
        {
            var favouriteObj = await _favouriteService.GetFavouriteByIdAsync(id);

            if (favouriteObj is null)
            {
                return NotFound();
            }

            return favouriteObj;
        }

        /**
         * Create new Favourite
         * POST: api/Favourite
         * 
         * @param newfavourite
         * @return Task<IActionResult>
         */
        [HttpPost]
        public async Task<IActionResult> CreateFavourite(Favourite newfavourite)
        {
            var response = await _favouriteService.CreateFavouriteAsync(newfavourite);

            return Ok(response);
        }

        /**
         * Get favourite by username
         * GET: api/Favourite/GetFavouriteByUsername/{username}
         * 
         * @param username
         * @return Task<ActionResult<Favourite>>
         */
        [Route("[action]/{username}")]
        [HttpGet]
        public async Task<ActionResult<Favourite>> GetFavouriteByUsername(string username)
        {

            var favouriteObj = await _favouriteService.GetFavouriteByUsernameAsync(username);

            if (favouriteObj is null)
            {
                return NoContent();
            }

            return favouriteObj;
        }

        /**
         * Get all favorites
         * GET: api/Favourite
         * 
         * @return Task<List<Favourite>>
         */
        [HttpGet]
        public async Task<List<Favourite>> GetAllFavourites() => await _favouriteService.GetAllFavouriteAsync();

        /**
         * Get all favourites by username
         * GET: api/Favourite/GetAllFavouriteByUsername/{username}
         * 
         * @return Task<List<Favourite>>
         */
        [Route("[action]/{username}")]
        [HttpGet]
        public async Task<List<Favourite>> GetAllFavouriteByUsername(string username)
        {

            var favouriteObj = await _favouriteService.GetAllFavouriteByUsernameAsync(username);

            if (favouriteObj is null)
            {
                return null;
            }

            return favouriteObj;
        }

        /**
         * Get all favourites by username with respect to station id
         * GET: api/Favourite/GetAllFavouriteByUsernameWithStationId/{username}
         * 
         * @param username
         * @return Task<List<Favourite>>
         */
        [Route("[action]/{username}")]
        [HttpGet]
        public async Task<List<FuelStation>> GetAllFavouriteByUsernameWithStationId(string username)
        {

            List<Favourite> favourites = await _favouriteService.GetAllFavouriteByUsernameAsync(username);
            List<FuelStation> fuelStations = new List<FuelStation>();

            foreach (Favourite favourite in favourites)
            {
                var id = favourite.StationId;
                var fuelStation = await _fuelStationService.GetAsync(id);

                fuelStations.Add(fuelStation);
            }

            return fuelStations;
        }

        /**
        * Delete favourite by station Id
        * DELETE: api/Favourite/DeleteFavouriteByStaionId/{id}
        * 
        * @param id
        * @return Task<IActionResult>
        */
        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFavouriteByStaionId(string id)
        {

            var response = await _favouriteService.GetFavouriteByStationId(id);
            if (response is null)
            {
                return NoContent();
            }
            else
            {
                await _favouriteService.DeleteFavouriteAsync(id);
                return Ok("Successfully Deleted");
            }

        }

        /**
         * Delete favourites by station Id
         * DELETE: api/Feedback/DeleteFeedbackByStationId/{id}
         * 
         * @param id
         * @retun Task<IActionResult>
         */
        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFavouritesByStationId(string id)
        {

            var feedback = await _favouriteService.DeleteFavouriteByStationIdAsync(id);

            if (feedback is null)
            {
                return NoContent();
            }

            return Ok(feedback);
        }
    }
}
