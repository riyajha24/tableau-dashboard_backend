using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class InvoiceModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } // MongoDB ObjectId will be automatically generated and stored as a string

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public double Cost { get; set; }

    public string Phone { get; set; } = string.Empty;

    public DateTime Date { get; set; }
}
