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

        //get a log by id

        //get logs by owner's username

        //get logs by station id

        //add a log
    }
}

