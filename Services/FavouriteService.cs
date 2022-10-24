using FuelAppAPI.Models;
using FuelAppAPI.Models.Database;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Dynamic;

namespace FuelAppAPI.Services
{
    public class FavouriteService
    {
        //Create mongodb collection references
        private readonly IMongoCollection<Favourite> _favouriteCollection;
        private readonly IMongoCollection<BsonDocument> _collection;

        //Intialize database configuration
        public FavouriteService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings) {

            var monogClient = new MongoClient(fuelDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = monogClient.GetDatabase(fuelDatabaseSettings.Value.DatabaseName);

            _favouriteCollection = mongoDatabase.GetCollection<Favourite>(fuelDatabaseSettings.Value.FavouriteCollectionName);
            _collection = mongoDatabase.GetCollection<BsonDocument>(fuelDatabaseSettings.Value.FavouriteCollectionName);

        }

        //Get favourite by username
        public async Task<Favourite> GetFavouriteByUsernameAsync(string username) =>
            await _favouriteCollection.Find(x => x.Username == username).FirstOrDefaultAsync();

        //Get favourite by station Id
        public async Task<Favourite> GetFavouriteByStationId(string id) =>
           await _favouriteCollection.Find(x => x.StationId == id).FirstOrDefaultAsync();

        // Get favourite By Id
        public async Task<Favourite?> GetFavouriteByIdAsync(string id) => await _favouriteCollection.Find(res => res.Id == id).FirstOrDefaultAsync();

        //Get all favourites
        public async Task<List<Favourite>> GetAllFavouriteAsync() => await _favouriteCollection.Find(_ => true).ToListAsync();

        //Get all favourites by username
        public async Task<List<Favourite>> GetAllFavouriteByUsernameAsync(string username) => 
            await _favouriteCollection.Find(res => res.Username == username).ToListAsync();

        //Create a new favourite
        public async Task<string> CreateFavouriteAsync(Favourite favourite) {

            try
            {
              
                var filterBuilder = Builders<BsonDocument>.Filter;
                var filter = filterBuilder.Eq("StationId", favourite.StationId) & filterBuilder.Eq("Username", favourite.Username);

                var result = await _collection.Find(filter).FirstAsync();

                if (result != null)
                {
                    
                    dynamic alreadyAvailableStation = new ExpandoObject();
                    alreadyAvailableStation.message = "This station is already added";
                    string jsonAlreadyAvailableStation = Newtonsoft.Json.JsonConvert.SerializeObject(alreadyAvailableStation);

                    return jsonAlreadyAvailableStation;
                }

            }
            catch (InvalidOperationException)
            {
                await _favouriteCollection.InsertOneAsync(favourite);

                var filterBuilder = Builders<BsonDocument>.Filter;
                var filter = filterBuilder.Eq("StationId", favourite.StationId) & filterBuilder.Eq("Username", favourite.Username);

                var result = await _collection.Find(filter).FirstAsync();

                if (result != null)
                {
                    dynamic newStation = new ExpandoObject();
                    newStation.message = "Successfully Added!";
                    string jsonNewStation = Newtonsoft.Json.JsonConvert.SerializeObject(newStation);

                    return jsonNewStation;
                }

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

        // Delete favourite
        public async Task DeleteFavouriteAsync(string id) => await _favouriteCollection.DeleteOneAsync(res => res.StationId == id);


    }
}
