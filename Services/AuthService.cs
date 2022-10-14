using FuelAppAPI.Models;
using FuelAppAPI.Models.Auth;
using FuelAppAPI.Models.Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Dynamic;

namespace FuelAppAPI.Services
{
    public class AuthService
    {
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly IMongoCollection<User> _usersCollection;

        // Database Configuration
        public AuthService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings)
        {
            var mongoClient = new MongoClient(fuelDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(fuelDatabaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<BsonDocument>("User");
            _usersCollection = mongoDatabase.GetCollection<User>("User");
        }

        // User Login
        public string UserLogin(AuthenticateRequest request)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("Username", request.Username);
                var document = _collection.Find(filter).First();

                if (document != null)
                {
                    if (document["Password"] == request.Password)
                    {
                        dynamic user = new ExpandoObject();
                        user.Id = document["_id"];
                        user.Username = document["Username"];
                        user.FullName = document["FullName"];
                        user.Email = document["Email"];
                        user.Role = document["Role"];
                        user.message = "Correct Username and Password";
                        user.status = true;
                        string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);

                        return jsonUser;
                    }

                    dynamic incorrectPassword = new ExpandoObject();
                    incorrectPassword.message = "Incorrect Password";
                    incorrectPassword.status = false;
                    string jsonIncorrectPassword = Newtonsoft.Json.JsonConvert.SerializeObject(incorrectPassword);

                    return jsonIncorrectPassword;
                }
            }
            catch (InvalidOperationException)
            {
                dynamic notFoundError = new ExpandoObject();
                notFoundError.message = "User or Username Not Found";
                notFoundError.status = false;
                string jsonNotFoundError = Newtonsoft.Json.JsonConvert.SerializeObject(notFoundError);

                return jsonNotFoundError;
            }

            dynamic notFound = new ExpandoObject();
            notFound.message = "User Not Found";
            notFound.status = false;
            string jsonNotFound = Newtonsoft.Json.JsonConvert.SerializeObject(notFound);

            return jsonNotFound;
        }

        // User Registration
        public async Task<string> UserRegistration(User user)
        {
            try
            {
                var filterUsername = Builders<BsonDocument>.Filter.Eq("Username", user.Username);
                var documentUsername = await _collection.Find(filterUsername).FirstAsync();

                if (documentUsername != null)
                {
                    dynamic alreadyUser = new ExpandoObject();
                    alreadyUser.message = "Username Already Exist";
                    alreadyUser.status = false;
                    string jsonAlreadyUser = Newtonsoft.Json.JsonConvert.SerializeObject(alreadyUser);

                    return jsonAlreadyUser;
                }
            }
            catch (InvalidOperationException)
            {
                await _usersCollection.InsertOneAsync(user);

                var filter = Builders<BsonDocument>.Filter.Eq("Username", user.Username);
                var document = await _collection.Find(filter).FirstAsync();

                if (document != null)
                {
                    dynamic newUser = new ExpandoObject();
                    newUser.message = "User Registration Success";
                    newUser.status = true;
                    string jsonNewUser = Newtonsoft.Json.JsonConvert.SerializeObject(newUser);

                    return jsonNewUser;
                }

                dynamic newUserFail = new ExpandoObject();
                newUserFail.message = "User Registration Failed";
                newUserFail.status = true;
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(newUserFail);

                return json;
            }

            dynamic data = new ExpandoObject();
            data.message = "User Registration Error";
            data.status = false;
            string jsonError = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            return jsonError;
        }
    }
}