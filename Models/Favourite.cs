/**
 * EAD - FuelMe API
 * 
 * @author H.G. Malwatta - IT19240848
 * 
 */

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

/**
 * @author H.G. Malwatta - IT19240848
 * 
 * This model class is used to create feedback objects
 * 
 */

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
