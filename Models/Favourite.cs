using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FuelAppAPI.Models
{
    public class Favourite
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string StationId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string StationName { get; set; } = null!;
        public string StationAddress { get; set; } = null!;
        public string CreateAt { get; set; } = null!;
    }
}
