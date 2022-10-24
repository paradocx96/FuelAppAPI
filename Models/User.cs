using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/*
 * EAD - FuelMe APP API
 *
 * Model class for User
 * 
 * @author IT19180526 - S.A.N.L.D. Chandrasiri
 * @version 1.0
 */

namespace FuelAppAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Username { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}