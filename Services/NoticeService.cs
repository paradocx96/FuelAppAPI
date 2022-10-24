/*
 * EAD - FuelMe APP API
 *
 * @author IT19180526 - S.A.N.L.D. Chandrasiri
 * @version 1.0
 */

using FuelAppAPI.Models;
using FuelAppAPI.Models.Database;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

/*
* Service class for Notice that handle database operation
*
* @author IT19180526 - S.A.N.L.D. Chandrasiri
* @version 1.0
*/
namespace FuelAppAPI.Services
{
    public class NoticeService
    {
        // Database collections for Notice
        private readonly IMongoCollection<Notice> _noticesCollection;
        private readonly IMongoCollection<BsonDocument> _collection;

        // Database Configuration
        public NoticeService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                fuelDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                fuelDatabaseSettings.Value.DatabaseName);

            _noticesCollection = mongoDatabase.GetCollection<Notice>(fuelDatabaseSettings.Value.NoticeCollectionName);
            _collection = mongoDatabase.GetCollection<BsonDocument>(fuelDatabaseSettings.Value.NoticeCollectionName);
        }

        /**
         * This method handle the get all notices operation
         *
         * @return Task<List<Notice>>
         * @see #GetAsync()
         */
        public async Task<List<Notice>> GetAsync() =>
            await _noticesCollection.Find(_ => true).ToListAsync();

        /**
         * This method handle the get notice by id operation
         *
         * @return Task<Notice?>
         * @see #GetAsync(string id)
         */
        public async Task<Notice?> GetAsync(string id) =>
            await _noticesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        /**
         * This method handle the create notice operation
         *
         * @return Task
         * @see #CreateAsync(Notice newNotice)
         */
        public async Task CreateAsync(Notice newNotice) =>
            await _noticesCollection.InsertOneAsync(newNotice);

        /**
         * This method handle the update notice operation
         *
         * @return Task
         * @see #UpdateAsync(string id, Notice updateNotice)
         */
        public async Task UpdateAsync(string id, Notice updateNotice) =>
            await _noticesCollection.ReplaceOneAsync(x => x.Id == id, updateNotice);

        /**
         * This method handle the delete notice operation
         *
         * @return Task
         * @see #RemoveAsync(string id)
         */
        public async Task RemoveAsync(string id) =>
            await _noticesCollection.DeleteOneAsync(x => x.Id == id);

        /**
         * This method handle the get notices by station id operation
         *
         * @return List<Notice>
         * @see #GetNoticesByStationId(string id)
         */
        public List<Notice> GetNoticesByStationId(string id)
        {
            // Create filter for to get notice by station id
            var filter = Builders<BsonDocument>.Filter.Eq("StationId", id);
            var cursor = _collection.Find(filter).ToCursor();

            // Create notice list object
            var notices = new List<Notice>();

            // Add notices to arraylist get from cursor object
            foreach (var document in cursor.ToEnumerable())
            {
                // Add notice to arraylist
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

        /**
         * This method handle the get notices by author (username) operation
         *
         * @return List<Notice>
         * @see #GetNoticesByAuthor(string author)
         */
        public List<Notice> GetNoticesByAuthor(string author)
        {
            // Create filter for to get notice by author (username) 
            var filter = Builders<BsonDocument>.Filter.Eq("Author", author);
            var cursor = _collection.Find(filter).ToCursor();

            // Create notice list object
            var notices = new List<Notice>();

            // Add notices to arraylist get from cursor object
            foreach (var document in cursor.ToEnumerable())
            {
                // Add notice to arraylist
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