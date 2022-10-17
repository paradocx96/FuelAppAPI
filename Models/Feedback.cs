using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FuelAppAPI.Models
{
    public class Feedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string StationId { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CreateAt { get; set; } = null!;

    }
}
