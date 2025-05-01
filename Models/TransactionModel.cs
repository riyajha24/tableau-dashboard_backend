using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class TransactionModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } // MongoDB ObjectId will be automatically generated and stored as a string

    public string TxId { get; set; } = string.Empty; // Transaction ID

    public string User { get; set; } = string.Empty; // Username of the user making the transaction

    public DateTime Date { get; set; } // Date of the transaction

    public double Cost { get; set; } // Cost of the transaction
}