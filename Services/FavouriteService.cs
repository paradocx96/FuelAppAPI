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

        public FavouriteService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings) {

            var monogClient = new MongoClient(fuelDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = monogClient.GetDatabase(fuelDatabaseSettings.Value.DatabaseName);

            _favouriteCollection = mongoDatabase.GetCollection<Favourite>(fuelDatabaseSettings.Value.FavouriteCollectionName);
            _collection = mongoDatabase.GetCollection<BsonDocument>(fuelDatabaseSettings.Value.FavouriteCollectionName);

        }
    }
}
