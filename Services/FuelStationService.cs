using System;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Dynamic;
using FuelAppAPI.Models;
using FuelAppAPI.Models.Database;

namespace FuelAppAPI.Services
{
    public class FuelStationService
    {
        //readonly collection for fuel stations
        private readonly IMongoCollection<FuelStation> _fuelStationCollection;

        //database configuration
        public FuelStationService(IOptions<FuelDatabaseSettings> fuelDatabaseSettings)
        {
            var mongoClient = new MongoClient(fuelDatabaseSettings.Value.ConnectionString); //initialize mongo client
            var mongoDatabase = mongoClient.GetDatabase(fuelDatabaseSettings.Value.DatabaseName); //initialize mongo database
            _fuelStationCollection = mongoDatabase.GetCollection<FuelStation>(fuelDatabaseSettings.Value.FuelStationsCollectionName); //assign the collection
        }

        //get all fuel stations
        public async Task<List<FuelStation>> GetAsync() =>
            await _fuelStationCollection.Find(_ => true).ToListAsync();

        //get a station by id
        public async Task<FuelStation?> GetAsync(string id) =>
            await _fuelStationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        //create a new fuel station entry
        public async Task CreateAsync(FuelStation newFuelStation) =>
            await _fuelStationCollection.InsertOneAsync(newFuelStation);

        //update a fuel station
        public async Task UpdateAsync(string id, FuelStation fuelStationToUpdate) =>
            await _fuelStationCollection.ReplaceOneAsync(x => x.Id == id, fuelStationToUpdate);

        //delete a fuel station entry
        public async Task DeleteAsync(string id) =>
            await _fuelStationCollection.DeleteOneAsync(x => x.Id == id);
    }
}

