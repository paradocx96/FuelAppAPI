/**
 * EAD - FuelMe API
 * 
 * @author H.G. Malwatta - IT19240848
 * 
 */

using FuelAppAPI.Models;
using FuelAppAPI.Models.Database;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Dynamic;

/**
 * @author H.G. Malwatta - IT19240848
 * 
 * This service class is used to manipulate all favourite related methods
 * 
 */
namespace FuelAppAPI.Services
{
    public class FavouriteService
    {
        //Create mongodb collection references
        private readonly IMongoCollection<Favourite> _favouriteCollection;
        private readonly IMongoCollection<BsonDocument> _collection;

        /**
        * Overloaded constructor
        * 
        * @param fuelDatabaseSettings
        */
        public FavouriteService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings) {

            var monogClient = new MongoClient(fuelDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = monogClient.GetDatabase(fuelDatabaseSettings.Value.DatabaseName);

            _favouriteCollection = mongoDatabase.GetCollection<Favourite>(fuelDatabaseSettings.Value.FavouriteCollectionName);
            _collection = mongoDatabase.GetCollection<BsonDocument>(fuelDatabaseSettings.Value.FavouriteCollectionName);

        }

        /**
         * Get favourite by username async
         * 
         * @param username
         * @return Task<Favourite>
         */
        public async Task<Favourite> GetFavouriteByUsernameAsync(string username) =>
            await _favouriteCollection.Find(x => x.Username == username).FirstOrDefaultAsync();

        /**
         * Get favourite by station Id async
         * 
         * @param id
         * @return Task<Favourite>
         */
        public async Task<Favourite> GetFavouriteByStationId(string id) =>
           await _favouriteCollection.Find(x => x.StationId == id).FirstOrDefaultAsync();

        /**
        * Get favourite By Id async
        * 
        * @param id
        * @return Task<Favourite>
        */
        public async Task<Favourite?> GetFavouriteByIdAsync(string id) => await _favouriteCollection.Find(res => res.Id == id).FirstOrDefaultAsync();


        /**
        * Get all favourites async
        * 
        * @return Task<List<Favourite>>
        */
        public async Task<List<Favourite>> GetAllFavouriteAsync() => await _favouriteCollection.Find(_ => true).ToListAsync();

 
        /**
        * Get all favourites by username async
        * 
        * @return Task<List<Favourite>>
        */
        public async Task<List<Favourite>> GetAllFavouriteByUsernameAsync(string username) => 
            await _favouriteCollection.Find(res => res.Username == username).ToListAsync();


        /**
        * Create a new favourite async
        * 
        * @param favourite
        * @return Task<string>
        */
        public async Task<string> CreateFavouriteAsync(Favourite favourite) {

            try
            {
              
                //Create filter object
                var filterBuilder = Builders<BsonDocument>.Filter;

                //Get data from database where station Id and username are equal
                var filter = filterBuilder.Eq("StationId", favourite.StationId) & filterBuilder.Eq("Username", favourite.Username);

                //Find the filtered data
                var result = await _collection.Find(filter).FirstAsync();

                //Check the result is null or not
                if (result != null)
                {
                    
                    //If result is not null, retun error message
                    dynamic alreadyAvailableStation = new ExpandoObject();
                    alreadyAvailableStation.message = "This station is already added";
                    string jsonAlreadyAvailableStation = Newtonsoft.Json.JsonConvert.SerializeObject(alreadyAvailableStation);

                    return jsonAlreadyAvailableStation;
                }

            }
            catch (InvalidOperationException)
            {
                //Add new favourite details into the database
                await _favouriteCollection.InsertOneAsync(favourite);

                //Create filter object
                var filterBuilder = Builders<BsonDocument>.Filter;

                //Get data from database where station Id and username are equal
                var filter = filterBuilder.Eq("StationId", favourite.StationId) & filterBuilder.Eq("Username", favourite.Username);

                //Find the filtered data
                var result = await _collection.Find(filter).FirstAsync();

                //Check the result is null or not
                if (result != null)
                {
                    //If result is not null, retun success message
                    dynamic newStation = new ExpandoObject();
                    newStation.message = "Successfully Added!";
                    string jsonNewStation = Newtonsoft.Json.JsonConvert.SerializeObject(newStation);

                    return jsonNewStation;
                }

                //If result is null, retun error message
                dynamic newStationFail = new ExpandoObject();
                newStationFail.message = "Something went wrong!";
                string jsonNewStationFail = Newtonsoft.Json.JsonConvert.SerializeObject(newStationFail);

                return jsonNewStationFail;
            }

            dynamic station = new ExpandoObject();
            station.message = "Something went wrong!";
            string jsonStationError = Newtonsoft.Json.JsonConvert.SerializeObject(station);

            return jsonStationError;
        }

        /**
        * Delete favourite by id async
        * 
        * @return Task
        */
        public async Task DeleteFavouriteAsync(string id) => await _favouriteCollection.DeleteOneAsync(res => res.StationId == id);


    }
}
