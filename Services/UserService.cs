using FuelAppAPI.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FuelAppAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        // Database Configuration
        public UserService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                fuelDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                fuelDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>("User");
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
    }
}
