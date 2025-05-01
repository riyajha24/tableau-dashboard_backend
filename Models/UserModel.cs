using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReactApp1.Server.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } // MongoDB ObjectId

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty; // Store hashed passwords
    }
}
