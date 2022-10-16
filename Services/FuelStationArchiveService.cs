using System;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Dynamic;
using FuelAppAPI.Models;
using FuelAppAPI.Models.Database;
using static MongoDB.Driver.WriteConcern;
/*
 * 
 * IT19014128
 * A.M.W.W.R.L. Wataketiya
 * 
 * Service class for fuel station archive
 * When deleting a fuel station entry an arhive entry is entered using this service
 */
namespace FuelAppAPI.Services
{
    public class FuelStationArchiveService
    {
        //readonly collection for mongo fuel station archive
        private readonly IMongoCollection<FuelStationArchive> _fuelStationsArchiveCollection;

        //database configuration
        public FuelStationArchiveService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings)
        {
            var mongoClient = new MongoClient(fuelDatabaseSettings.Value.ConnectionString); //initialize mongo client
            var mongoDatabase = mongoClient.GetDatabase(fuelDatabaseSettings.Value.DatabaseName); //initialize mongo database
        }
    }
}

