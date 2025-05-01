using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReactApp1.Server.Models
{
    public class PieDataModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Label { get; set; }
        public int Value { get; set; }
        public string Color { get; set; }

        // This property will return the Id as a string
        [BsonIgnore]
        public string IdString => Id.ToString();
    }
}
