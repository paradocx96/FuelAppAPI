﻿using System;
using FuelAppAPI.Models;
using FuelAppAPI.Models.Database;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FuelAppAPI.Services
{
    public class QueueLogService
    {
        //readonly collection for queue logs
        private readonly IMongoCollection<QueueLogItem> _queueLogItemCollection;
        //database configuration
        public QueueLogService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings)
        {
            var mongoClient = new MongoClient(fuelDatabaseSettings.Value.ConnectionString); //initialize mongo client
            var mongoDatabase = mongoClient.GetDatabase(fuelDatabaseSettings.Value.DatabaseName); //initialize mongo database
            _queueLogItemCollection = mongoDatabase.GetCollection<QueueLogItem>(fuelDatabaseSettings.Value.QueueLogsCollectionName); //assign the collection
        }

        //get all logs
        public async Task<List<QueueLogItem>> GetAsync() =>
            await _queueLogItemCollection.Find(_ => true).ToListAsync();

        //get a log by id
        public async Task<QueueLogItem?> GetAsync(string id) =>
            await _queueLogItemCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        //get logs by cutomer username
        public async Task<QueueLogItem?> GetByUsername(string username) =>
            await _queueLogItemCollection.Find(x => x.CustomerUsername == username).FirstOrDefaultAsync();

        //get logs by station id
        public async Task<QueueLogItem?> GetByStatioId(string stationId) =>
            await _queueLogItemCollection.Find(x => x.StationId == stationId).FirstOrDefaultAsync();

        //add a log
        public async Task CreateAsync(QueueLogItem queueLogItem) =>
            await _queueLogItemCollection.InsertOneAsync(queueLogItem);
    }
}

