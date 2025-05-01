using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class TeamModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } // Make the Id field nullable

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int Age { get; set; }

    public string Phone { get; set; } = string.Empty;

    public string Access { get; set; } = string.Empty;
}
