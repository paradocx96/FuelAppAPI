/*
 * EAD - FuelMe APP API
 *
 * @author IT19180526 - S.A.N.L.D. Chandrasiri
 * @version 1.0
 */

using FuelAppAPI.Models;
using FuelAppAPI.Models.Auth;
using FuelAppAPI.Models.Database;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Dynamic;

/*
* Service class for Auth (Login/Registration) that handle database operation 
*
* @author IT19180526 - S.A.N.L.D. Chandrasiri
* @version 1.0
*/
namespace FuelAppAPI.Services
{
    public class AuthService
    {
        // Database collections for User
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly IMongoCollection<User> _usersCollection;

        // Database Configuration
        public AuthService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings)
        {
            var mongoClient = new MongoClient(fuelDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(fuelDatabaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<BsonDocument>(fuelDatabaseSettings.Value.UserCollectionName);
            _usersCollection = mongoDatabase.GetCollection<User>(fuelDatabaseSettings.Value.UserCollectionName);
        }

        /**
         * This method handle the User Login operation
         *
         * @return string JSON object
         * @see #UserLogin(AuthenticateRequest request)
         */
        public string UserLogin(AuthenticateRequest request)
        {
            try
            {
                // Create filter for check username availability in database
                var filter = Builders<BsonDocument>.Filter.Eq("Username", request.Username);
                var document = _collection.Find(filter).First();

                // Check username availability 
                if (document != null)
                {
                    // Check password is correct
                    if (document["Password"] == request.Password)
                    {
                        // Create JSON object for send successes response
                        dynamic user = new ExpandoObject();
                        user.Id = document["_id"];
                        user.Username = document["Username"];
                        user.FullName = document["FullName"];
                        user.Email = document["Email"];
                        user.Role = document["Role"];
                        user.message = "Correct Username and Password";
                        user.status = true;

                        // Convert JSON object to string 
                        string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);

                        return jsonUser;
                    }

                    // Create JSON object for send password incorrect response
                    dynamic incorrectPassword = new ExpandoObject();
                    incorrectPassword.message = "Incorrect Password";
                    incorrectPassword.status = false;

                    // Convert JSON object to string 
                    string jsonIncorrectPassword = Newtonsoft.Json.JsonConvert.SerializeObject(incorrectPassword);

                    return jsonIncorrectPassword;
                }
            }
            catch (InvalidOperationException)
            {
                // Create JSON object for send user not available response
                dynamic notFoundError = new ExpandoObject();
                notFoundError.message = "User or Username Not Found";
                notFoundError.status = false;

                // Convert JSON object to string
                string jsonNotFoundError = Newtonsoft.Json.JsonConvert.SerializeObject(notFoundError);

                return jsonNotFoundError;
            }

            // Create JSON object for send user not available response
            dynamic notFound = new ExpandoObject();
            notFound.message = "User Not Found";
            notFound.status = false;

            // Convert JSON object to string
            string jsonNotFound = Newtonsoft.Json.JsonConvert.SerializeObject(notFound);

            return jsonNotFound;
        }

        /**
         * This method handle the User Registration operation
         *
         * @return Task<string>
         * @see #UserRegistration(User user)
         */
        public async Task<string> UserRegistration(User user)
        {
            try
            {
                // Create filter for check username availability in database before create user
                var filterUsername = Builders<BsonDocument>.Filter.Eq("Username", user.Username);
                var documentUsername = await _collection.Find(filterUsername).FirstAsync();

                // Check username availability 
                if (documentUsername != null)
                {
                    // Create JSON object for send username available response
                    dynamic alreadyUser = new ExpandoObject();
                    alreadyUser.message = "Username Already Exist";
                    alreadyUser.status = false;

                    // Convert JSON object to string
                    string jsonAlreadyUser = Newtonsoft.Json.JsonConvert.SerializeObject(alreadyUser);

                    return jsonAlreadyUser;
                }
            }
            catch (InvalidOperationException)
            {
                // Create User in database
                await _usersCollection.InsertOneAsync(user);

                // Create filter for check username availability in database after create user
                var filter = Builders<BsonDocument>.Filter.Eq("Username", user.Username);
                var document = await _collection.Find(filter).FirstAsync();

                // Check username availability 
                if (document != null)
                {
                    // Create JSON object for send user creation successes response
                    dynamic newUser = new ExpandoObject();
                    newUser.message = "User Registration Success";
                    newUser.status = true;

                    // Convert JSON object to string
                    string jsonNewUser = Newtonsoft.Json.JsonConvert.SerializeObject(newUser);

                    return jsonNewUser;
                }

                // Create JSON object for send user creation failed response
                dynamic newUserFail = new ExpandoObject();
                newUserFail.message = "User Registration Failed";
                newUserFail.status = true;

                // Convert JSON object to string
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(newUserFail);

                return json;
            }

            // Create JSON object for send user creation error response
            dynamic data = new ExpandoObject();
            data.message = "User Registration Error";
            data.status = false;

            // Convert JSON object to string
            string jsonError = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            return jsonError;
        }
    }
}