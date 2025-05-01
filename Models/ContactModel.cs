using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReactApp1.Server.Models
{
    public class ContactModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } // MongoDB ObjectId will be automatically generated and stored as a string

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;

        public int RegistrarId { get; set; }
    }
}
