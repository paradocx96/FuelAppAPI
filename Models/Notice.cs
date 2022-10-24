using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/*
 * EAD - FuelMe APP API
 *
 * Model class for Notice
 * 
 * @author IT19180526 - S.A.N.L.D. Chandrasiri
 * @version 1.0
 */

namespace FuelAppAPI.Models
{
    public class Notice
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string StationId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string CreateAt { get; set; } = null!;
    }
}