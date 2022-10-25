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
* Service class for User that handle database operation
*
* @author IT19180526 - S.A.N.L.D. Chandrasiri
* @version 1.0 
*
* Reference:
* https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-6.0&tabs=visual-studio
* https://mongodb.github.io/mongo-csharp-driver/
*/
namespace FuelAppAPI.Services
{
    public class UserService
    {
        // Database collections for User
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

        /**
         * This method handle the get all Users operation
         *
         * @return Task<List<User>>
         * @see #GetAsync()
         */
        public async Task<List<User>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        /**
         * This method handle the get user by id operation
         *
         * @return Task<User?>
         * @see #GetAsync(string id)
         */
        public async Task<User?> GetAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        /**
         * This method handle the update user operation
         *
         * @return Task
         * @see #UpdateAsync(string id, User updateUser)
         */
        public async Task UpdateAsync(string id, User updateUser) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updateUser);

        /**
         * This method handle the delete user operation
         *
         * @return Task
         * @see #RemoveAsync(string id)
         */
        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);

        /**
         * This method handle the get users by role operation
         *
         * @return List<User>
         * @see #GetUsersByRole(string role)
         */
        public List<User> GetUsersByRole(string role)
        {
            // Create filter for to get users by role
            var filter = Builders<BsonDocument>.Filter.Eq("Role", role);
            var cursor = _collection.Find(filter).ToCursor();

            // Create user list object
            var users = new List<User>();

            // Add users to arraylist get from cursor object
            foreach (var document in cursor.ToEnumerable())
            {
                // Add user to arraylist
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