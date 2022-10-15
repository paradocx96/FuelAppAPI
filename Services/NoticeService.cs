using FuelAppAPI.Models;
using FuelAppAPI.Models.Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FuelAppAPI.Services
{
    public class NoticeService
    {
        private readonly IMongoCollection<Notice> _noticesCollection;
        private readonly IMongoCollection<BsonDocument> _collection;

        // Database Configuration
        public NoticeService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                fuelDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                fuelDatabaseSettings.Value.DatabaseName);

            _noticesCollection = mongoDatabase.GetCollection<Notice>("Notice");
            _collection = mongoDatabase.GetCollection<BsonDocument>("Notice");
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

        // Get All Notices By Station Id
        public List<Notice> GetNoticesByStationId(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("StationId", id);
            var cursor = _collection.Find(filter).ToCursor();

            var notices = new List<Notice>();
            
            foreach (var document in cursor.ToEnumerable())
            {
                notices.Add(new Notice
                {
                    Id = document[0].ToString()!,
                    StationId = document[1].ToString()!,
                    Title = document[2].ToString()!,
                    Description = document[3].ToString()!,
                    Author = document[4].ToString()!,
                    CreateAt = document[5].ToString()!
                });
            }

            return notices;
        }
    }
}