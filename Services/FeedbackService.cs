
/**
 * EAD - FuelMe API
 * 
 * @author H.G. Malwatta - IT19240848
 * @references
 * - https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-6.0&tabs=visual-studio
 * - https://mongodb.github.io/mongo-csharp-driver/2.18/getting_started/quick_tour/
 * - https://mongodb.github.io/mongo-csharp-driver/
 * 
 */

using FuelAppAPI.Models;
using FuelAppAPI.Models.Database;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Dynamic;

/**
 * @author H.G. Malwatta - IT19240848
 * 
 * This service class is used to manipulate all feedback related methods
 * 
 */
namespace FuelAppAPI.Services
{
    public class FeedbackService
    {
        private readonly IMongoCollection<Feedback> _feedbackCollection;
        private readonly IMongoCollection<BsonDocument> _collection;

        /**
         * Overloaded constroctor
         * 
         * @param fuelDatabaseSettings
         */
        public FeedbackService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings)
        {
            //Initialize data base configuration
            var monogClient = new MongoClient(fuelDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = monogClient.GetDatabase(fuelDatabaseSettings.Value.DatabaseName);

            _feedbackCollection = mongoDatabase.GetCollection<Feedback>(fuelDatabaseSettings.Value.FeedbackCollectionName);
            _collection = mongoDatabase.GetCollection<BsonDocument>(fuelDatabaseSettings.Value.FeedbackCollectionName);
        }

        /**
         * Get All Feedbacks async
         * 
         * @retun Task<List<Feedback>>
         */
        public async Task<List<Feedback>> GetAllFeedbacksAsync() => await _feedbackCollection.Find(_ => true).ToListAsync();


        /**
         * Get Feedback By Id async
         * 
         * @param id
         * @retun Task<Feedback?>
         */
        public async Task<Feedback?> GetFeedbackByIdAsync(string id) => await _feedbackCollection.Find(res => res.Id == id).FirstOrDefaultAsync();


        /**
         * Create new Feedback async
         * 
         * @param feedback
         * @retun Task
         */
        public async Task CreateFeedbackAsync(Feedback feedback) => await _feedbackCollection.InsertOneAsync(feedback);


        /**
         * Update Feedback async
         * 
         * @param id
         * @param feedback
         * @retun Task
         */
        public async Task UpdateFeedbackAsync(string id, Feedback updateFeedback) => await _feedbackCollection.ReplaceOneAsync(res => res.Id == id, updateFeedback);

 
        /**
         * Delete Feedback async
         * 
         * @param id
         * @retun Task
         */
        public async Task DeleteFeedbackAsync(string id) => await _feedbackCollection.DeleteOneAsync(res => res.Id == id);


        /**
         * Get all Feedbacks by station Id async
         * 
         * @param id
         * @retun List<Feedback>
         */
        public List<Feedback> GetAllFeedbackByStationId(string id)
        {
            //Filter data from database where station id is equal
            var filterStationFeedback = Builders<BsonDocument>.Filter.Eq("StationId", id);

            //Find using filtered object
            var cursor = _collection.Find(filterStationFeedback).ToCursor();
            var feedback = new List<Feedback>();

            //Assign each feedback object value to array list
            foreach (var document in cursor.ToEnumerable())
            {
                feedback.Add(new Feedback
                {
                    Id = document[0].ToString()!,
                    StationId = document[1].ToString()!,
                    Subject = document[2].ToString()!,
                    Username = document[3].ToString()!,
                    Description = document[4].ToString()!,
                    CreateAt = document[5].ToString()!
                }) ;
            }

            return feedback;
  
        }

        /**
         * Delete Feedbacks by station Id async
         * 
         * @param id
         * @retun List<string>
         */
        public async Task<string> DeleteFeeedbackByStationIdAsync(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("StationId", id);

            var result = await _collection.DeleteManyAsync(filter);

            if (result == null)
            {
                //If result is not null, retun error message
                dynamic errorMsg = new ExpandoObject();
                errorMsg.message = "Some thing went wrong!";
                string jsonErrorMsg = Newtonsoft.Json.JsonConvert.SerializeObject(errorMsg);

                return jsonErrorMsg;
            }
            else
            {
                //If result is not null, retun success message
                dynamic successMsg = new ExpandoObject();
                successMsg.message = "Successfully deleted!";
                string jsonSuccessMsg = Newtonsoft.Json.JsonConvert.SerializeObject(successMsg);

                return jsonSuccessMsg;
            }

        }

    }
}
 