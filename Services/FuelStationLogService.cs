using System;
using FuelAppAPI.Models;
using FuelAppAPI.Models.Database;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

/*
 * IT19014128
 * A.M.W.W.R.L. Wataketiya
 * 
 * Service class for Fuel station logs
 * 
 * References:
 * https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0
 * 
 */

namespace FuelAppAPI.Services
{
    public class FuelStationLogService
    {
        //readonly collection for fuel station logs
        private readonly IMongoCollection<FuelStationLogItem> _fuelStationLogItemCollection;
        public FuelStationLogService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings)
        {
            var mongoClient = new MongoClient(fuelDatabaseSettings.Value.ConnectionString); //initialize mongo client
            var mongoDatabase = mongoClient.GetDatabase(fuelDatabaseSettings.Value.DatabaseName); //initialize mongo database
            _fuelStationLogItemCollection = mongoDatabase.GetCollection<FuelStationLogItem>(fuelDatabaseSettings.Value.FuelStationLogsCollectionName); //assign the collection
        }

        //get all logs
        public async Task<List<FuelStationLogItem>> GetAsync() =>
            await _fuelStationLogItemCollection.Find(_ => true).ToListAsync();

        //get a log by id
        public async Task<FuelStationLogItem> GetAsync(string id) =>
            await _fuelStationLogItemCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        //get logs by station id
        public async Task<List<FuelStationLogItem>> GetByStationId(string stationId) =>
            await _fuelStationLogItemCollection.Find(x => x.StationId == stationId).ToListAsync();

        //add a log
        public async Task CreateAsync(FuelStationLogItem fuelStationLogItem) =>
            await _fuelStationLogItemCollection.InsertOneAsync(fuelStationLogItem);
    }
}

