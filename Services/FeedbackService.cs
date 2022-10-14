using FuelAppAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Dynamic;

namespace FuelAppAPI.Services
{
    public class FeedbackService
    {
        private readonly IMongoCollection<Feedback> _feedbackCollection;
        private readonly IMongoCollection<BsonDocument> _collection;

        //Database Configuartion
        public FeedbackService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings)
        {
            var monogClient = new MongoClient(fuelDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = monogClient.GetDatabase(fuelDatabaseSettings.Value.DatabaseName);

            _feedbackCollection = mongoDatabase.GetCollection<Feedback>("Feedback");
            _collection = mongoDatabase.GetCollection<BsonDocument>("Feedback");
        }

        //Get All Feedbacks
        public async Task<List<Feedback>> GetAllFeedbacksAsync() => await _feedbackCollection.Find(_ => true).ToListAsync();

        // Get Feedback By Id
        public async Task<Feedback?> GetFeedbackByIdAsync(string id) => await _feedbackCollection.Find(res => res.Id == id).FirstOrDefaultAsync();


        // Create New Feedback
        public async Task CreateFeedbackAsync(Feedback feedback) => await _feedbackCollection.InsertOneAsync(feedback);

        // Update Feedback
        public async Task UpdateFeedbackAsync(string id, Feedback updateFeedback) => await _feedbackCollection.ReplaceOneAsync(res => res.Id == id, updateFeedback);

        // Delete Feedback
        public async Task DeleteFeedbackAsync(string id) => await _feedbackCollection.DeleteOneAsync(res => res.Id == id);


        //---------- Station Owner
        //Get All Feedbacks By Station ID
        public List<Feedback> GetAllFeedbackByStationId(string id)
        {
            var filterStationFeedback = Builders<BsonDocument>.Filter.Eq("StationId", id);
            var cursor = _collection.Find(filterStationFeedback).ToCursor();
            var feedback = new List<Feedback>();

            foreach (var document in cursor.ToEnumerable())
            {
                feedback.Add(new Feedback
                {
                    Id = document[0].ToString()!,
                    StationId = document[1].ToString()!,
                    Username = document[2].ToString()!,
                    Description = document[3].ToString()!,
                    CreateAt = document[4].ToString()!
                }) ;
            }

            return feedback;
  
        }

    }
}
 