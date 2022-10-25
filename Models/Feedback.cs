/**
 * EAD - FuelMe API
 * 
 * @author H.G. Malwatta - IT19240848
 * 
 * @references
 * - https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-6.0&tabs=visual-studio
 */

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/**
 * @author H.G. Malwatta - IT19240848
 * 
 * This model class is used to create feedback object
 * 
 */
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
