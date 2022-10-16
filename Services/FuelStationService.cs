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
 * This is the service class for fuel stations where the mongodb connections are handled
 */

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


        //get station by owner's username
        public async Task<FuelStation> GetStationByOwnerUsernameAsync(string username) =>
            await _fuelStationCollection.Find(x => x.OwnerUsername == username).FirstOrDefaultAsync();

        //update petrol queue length
        public async Task UpdatePetrolQueueLength(string id, int newLength)
        {
            var filter = Builders<FuelStation>.Filter.Eq("Id", id); //set the filter to get the station by id
            var update = Builders<FuelStation>.Update.Set("PetrolQueueLength", newLength); //set the update to the length

            await _fuelStationCollection.FindOneAndUpdateAsync(filter, update);
        }

        //update diesel queue length
        public async Task UpdateDieselQueueLength(string id, int newLength)
        {
            var filter = Builders<FuelStation>.Filter.Eq("Id", id); //set the filter to get the station by id
            var update = Builders<FuelStation>.Update.Set("DieselQueueLength", newLength); //set the update to the length

            await _fuelStationCollection.FindOneAndUpdateAsync(filter, update);

        }

        //increment petrol queue length
        public async Task IncrementPetrolQueueLength(string id)
        {
            var station = await GetAsync(id);
            int? newLength = station.PetrolQueueLength;
            newLength++; //increment the length
            var filter = Builders<FuelStation>.Filter.Eq("Id", id); //set the filter to get the station by id
            var update = Builders<FuelStation>.Update.Set("PetrolQueueLength", newLength); //set the update to the length

            await _fuelStationCollection.FindOneAndUpdateAsync(filter, update);
        }

        //decrement petrol queue length
        public async Task DecrementPetrolQueueLength(string id)
        {
            var station = await GetAsync(id);
            int? newLength = station.PetrolQueueLength;
            newLength--; //decrement the length
            var filter = Builders<FuelStation>.Filter.Eq("Id", id); //set the filter to get the station by id
            var update = Builders<FuelStation>.Update.Set("PetrolQueueLength", newLength); //set the update to the length
            await _fuelStationCollection.FindOneAndUpdateAsync(filter, update);
        }

        //increment diesel queue length
        public async Task IncrementDieselQueueLength(string id)
        {
            var station = await GetAsync(id);
            int? newLength = station.DieselQueueLength;
            newLength++; //increment the length
            var filter = Builders<FuelStation>.Filter.Eq("Id", id); //set the filter to get the station by id
            var update = Builders<FuelStation>.Update.Set("DieselQueueLength", newLength); //set the update to the length

            await _fuelStationCollection.FindOneAndUpdateAsync(filter, update);
        }

        //decrement diesel queue length
        public async Task DecrementDieselQueueLength(string id)
        {
            var station = await GetAsync(id);
            int? newLength = station.DieselQueueLength;
            newLength--; //increment the length
            var filter = Builders<FuelStation>.Filter.Eq("Id", id); //set the filter to get the station by id
            var update = Builders<FuelStation>.Update.Set("DieselQueueLength", newLength); //set the update to the length

            await _fuelStationCollection.FindOneAndUpdateAsync(filter, update);
        }

        //update petrol status
        public async Task UpdatePetrolStatus(string id, string newStatus)
        {
            var filter = Builders<FuelStation>.Filter.Eq("Id", id); //set the filter to get the station by id
            var update = Builders<FuelStation>.Update.Set("PetrolStatus", newStatus); //set the update to the new petrol status

            await _fuelStationCollection.FindOneAndUpdateAsync(filter, update);

        }

        //update diesel status
        public async Task UpdateDieselStatus(string id, string newStatus)
        {
            var filter = Builders<FuelStation>.Filter.Eq("Id", id); //set the filter to get the station by id
            var update = Builders<FuelStation>.Update.Set("DieselStatus", newStatus); //set the update to the new diesel status

            await _fuelStationCollection.FindOneAndUpdateAsync(filter, update);

        }

        //update station open status
        public async Task UpdateStationOpenStatus(string id, string newStatus)
        {
            var filter = Builders<FuelStation>.Filter.Eq("Id", id); //set the filter to get the station by id
            var update = Builders<FuelStation>.Update.Set("OpenStatus", newStatus); //set the update to the new open status

            await _fuelStationCollection.FindOneAndUpdateAsync(filter, update);

        }
    }
}

