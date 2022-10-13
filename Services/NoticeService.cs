using FuelAppAPI.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FuelAppAPI.Services
{
    public class NoticeService
    {
        private readonly IMongoCollection<Notice> _noticesCollection;

        // Database Configuration
        public NoticeService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                fuelDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                fuelDatabaseSettings.Value.DatabaseName);

            _noticesCollection = mongoDatabase.GetCollection<Notice>("Notice");
        }

        // Get All Notices
        public async Task<List<Notice>> GetAsync() =>
            await _noticesCollection.Find(_ => true).ToListAsync();

        // Get Notice By Id
        public async Task<Notice?> GetAsync(string id) =>
            await _noticesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Create Notice
        public async Task CreateAsync(Notice newNotice) =>
            await _noticesCollection.InsertOneAsync(newNotice);

        // Update Notice
        public async Task UpdateAsync(string id, Notice updateNotice) =>
            await _noticesCollection.ReplaceOneAsync(x => x.Id == id, updateNotice);

        // Delete Notice
        public async Task RemoveAsync(string id) =>
            await _noticesCollection.DeleteOneAsync(x => x.Id == id);
    }
}