using System;
using FuelAppAPI.Models;
using FuelAppAPI.Models.Database;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FuelAppAPI.Services
{
    public class QueueLogService
    {
        private readonly IMongoCollection<QueueLogItem> _queueLogItemCollection;
        public QueueLogService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings)
        {
            var mongoClient = new MongoClient(fuelDatabaseSettings.Value.ConnectionString); //initialize mongo client
            var mongoDatabase = mongoClient.GetDatabase(fuelDatabaseSettings.Value.DatabaseName); //initialize mongo database
            _queueLogItemCollection = mongoDatabase.GetCollection<QueueLogItem>(fuelDatabaseSettings.Value.QueueLogsCollectionName); //assign the collection
        }
    }
}

