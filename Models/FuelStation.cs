using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace FuelAppAPI.Models
{
    public class FuelStation
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

        public string? OpenStatus { get; set; }
        public int? QueueLength { get; set; }
        public string? PetrolStatus { get; set; }
        public string? DieselStatus { get; set; }

        public double? LocationLatitude { get; set; }
        public double? LocationLongitude { get; set; }
    }
}

