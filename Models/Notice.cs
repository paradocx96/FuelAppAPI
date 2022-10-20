using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/*
* IT19180526
* S.A.N.L.D. Chandrasiri
* Model class for Notice
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