using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FuelAppAPI.Models;

public class Notice
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string StationId { get; set; } = null!;

    [BsonElement("Title")]
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Author { get; set; } = null!;
    
    public string CreateAt { get; set; } = null!;
}