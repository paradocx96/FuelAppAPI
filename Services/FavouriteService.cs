using FuelAppAPI.Models;
using FuelAppAPI.Models.Database;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

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

        //Create a new favourite
        public async Task CreateFavouriteAsync(Favourite newfavourite) =>
            await _favouriteCollection.InsertOneAsync(newfavourite);

        //Get favourite by username
        public async Task<Favourite> GetFavouriteByUsernameAsync(string username) =>
            await _favouriteCollection.Find(x => x.Username == username).FirstOrDefaultAsync();

        // Get favourite By Id
        public async Task<Favourite?> GetFavouriteByIdAsync(string id) => await _favouriteCollection.Find(res => res.Id == id).FirstOrDefaultAsync();

    }
}
