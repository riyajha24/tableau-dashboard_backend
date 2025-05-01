using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ReactApp1.Server.Models
{
    public class LineData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String MongoId { get; set; } // Internal MongoDB ObjectId

        [BsonElement("id")]
        public string? Id { get; set; } // String ID from your document

        [BsonElement("color")]
        public string? Color { get; set; }

        [BsonElement("data")]
        public List<LineDataPoint>? Data { get; set; }
    }

    public class LineDataPoint
    {
        [BsonElement("x")]
        public string? X { get; set; }

        [BsonElement("y")]
        public int? Y { get; set; }
    }
}
