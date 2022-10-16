using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/*
 * IT19014128
 * A.M.W.W.R.L. Wataketiya
 * 
 * Model class for Fuel Station Archive
 */
namespace FuelAppAPI.Models
{
    public class FuelStationArchive
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string? License { get; set; }
        public string? OwnerUsername { get; set; }
        public string? StationName { get; set; }
        public string? StationAddress { get; set; }
        public string? StationPhoneNumber { get; set; }
        public string? StationEmail { get; set; }
        public string? StationWebsite { get; set; }
    }
}

