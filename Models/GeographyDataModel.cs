using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReactApp1.Server.Models
{
    public class GeographyDataModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string IdCode { get; set; } // Corresponds to "id" in mock data
        public int Value { get; set; }

        // This property will return the Id as a string for convenience
        [BsonIgnore]
        public string IdString => Id.ToString();
    }
}
