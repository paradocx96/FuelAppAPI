﻿using System;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Dynamic;
using FuelAppAPI.Models;
using FuelAppAPI.Models.Database;
using static MongoDB.Driver.WriteConcern;

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

        //update queue count
        public async Task UpdateQueueLength(string id, int newLength)
        {
            var filter = Builders<FuelStation>.Filter.Eq("Id", id); //set the filter to get the station by id
            var update = Builders<FuelStation>.Update.Set("FuelStation", newLength); //set the update to the length

            await _fuelStationCollection.FindOneAndUpdateAsync(filter, update);
        }

        //increment queue count
        public async Task IncrementQueueLength(string id)
        {
            var station = await GetAsync(id);
            int? newLength = station.QueueLength;
            newLength++; //increment the length
            var filter = Builders<FuelStation>.Filter.Eq("Id", id); //set the filter to get the station by id
            var update = Builders<FuelStation>.Update.Set("FuelStation", newLength); //set the update to the length

            await _fuelStationCollection.FindOneAndUpdateAsync(filter, update);
        }

        //decrement queue count
        public async Task DecrementQueueLength(string id)
        {
            var station = await GetAsync(id);
            int? newLength = station.QueueLength;
            newLength--; //decrement the length
            var filter = Builders<FuelStation>.Filter.Eq("Id", id); //set the filter to get the station by id
            var update = Builders<FuelStation>.Update.Set("FuelStation", newLength); //set the update to the length
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

