﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/*
 * IT19014128
 * A.M.W.W.R.L. Wataketiya
 * 
 * Model for Fuel Station Log Item
 */

namespace FuelAppAPI.Models
{
    public class FuelStationLogItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? StationId { get; set; }
        public string? FuelType { get; set; }
        public string? FuelStatus { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
