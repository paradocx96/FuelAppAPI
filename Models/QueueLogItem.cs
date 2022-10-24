using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FuelAppAPI.Models
{
    public class QueueLogItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? CustomerUsername { get; set; }
        public string? StationId { get; set; }
        public string? StationLicense { get; set; }
        public string? StationName { get; set; }
        public string? Queue { get; set; }
        public string? Action { get; set; }  //join / leave
        public string? RefuelStatus { get; set; } //refuled / not-refueled / not-applicable
        public DateTime? dateTime { get; set; }
    }
}

