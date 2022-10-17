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
            _fuelStationsArchiveCollection = mongoDatabase.GetCollection<FuelStationArchive>(fuelDatabaseSettings.Value.FuelStationArchivesCollectionName); //assign the collection
        }

        //get all fuel station archives
        public async Task<List<FuelStationArchive>> GetAsync() =>
            await _fuelStationsArchiveCollection.Find(_ => true).ToListAsync();

        //get fuel station archive by name
        public async Task<FuelStationArchive> GetAsync(string id) =>
            await _fuelStationsArchiveCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        //add fuel station archive
        public async Task CreateAsync(FuelStationArchive fuelStationArchive) =>
            await _fuelStationsArchiveCollection.InsertOneAsync(fuelStationArchive);
    }
}

