using FuelAppAPI.Models;
using FuelAppAPI.Models.Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

/*
* IT19180526
* S.A.N.L.D. Chandrasiri
* Service class for User that handle database operation 
*/
namespace FuelAppAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IMongoCollection<BsonDocument> _collection;

        // Database Configuration
        public UserService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                fuelDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                fuelDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(fuelDatabaseSettings.Value.UserCollectionName);
            _collection = mongoDatabase.GetCollection<BsonDocument>(fuelDatabaseSettings.Value.UserCollectionName);
        }

        // Get Users
        public async Task<List<User>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        // Get User By Id
        public async Task<User?> GetAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Update User
        public async Task UpdateAsync(string id, User updateUser) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updateUser);
        
        // Delete User
        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
        
        // Get All Users By Role
        public List<User> GetUsersByRole(string role)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Role", role);
            var cursor = _collection.Find(filter).ToCursor();

            var users = new List<User>();
            
            foreach (var document in cursor.ToEnumerable())
            {
                users.Add(new User
                {
                    Id = document[0].ToString()!,
                    Username = document[1].ToString()!,
                    FullName = document[2].ToString()!,
                    Email = document[3].ToString()!,
                    Password = document[4].ToString()!,
                    Role = document[5].ToString()!
                });
            }

            return users;
        }
    }
}
