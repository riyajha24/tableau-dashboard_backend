using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using ReactApp1.Server.Models;

public class MongoDbService
{
    private readonly IMongoDatabase _database;

    public MongoDbService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase("ASPdotNet"); // Replace with your database name
    }

    // Method to get the TeamModel collection
    public IMongoCollection<TeamModel> GetTeamCollection()
    {
        return _database.GetCollection<TeamModel>("teams"); // Replace with your collection name
    }

    // Method to get the ContactModel collection
    public IMongoCollection<ContactModel> GetContactCollection()
    {
        return _database.GetCollection<ContactModel>("contacts"); // Replace with your collection name
    }

    // Method to get the InvoiceModel collection
    public IMongoCollection<InvoiceModel> GetInvoiceCollection()
    {
        return _database.GetCollection<InvoiceModel>("invoices"); // Replace with your collection name
    }

    // Method to get the LineData collection
    public IMongoCollection<LineData> GetLineDataCollection()
    {
        return _database.GetCollection<LineData>("lineData"); // Replace with your collection name
    }

    // Method to get the UserModel collection
    public IMongoCollection<UserModel> GetUserCollection()
    {
        return _database.GetCollection<UserModel>("users"); // Replace with your collection name
    }

    // Method to get the TransactionModel collection
    public IMongoCollection<TransactionModel> GetTransactionCollection()
    {
        return _database.GetCollection<TransactionModel>("transactions"); // Replace with your collection name
    }

    public IMongoCollection<BarDataModel> GetBarDataCollection()
    {
        return _database.GetCollection<BarDataModel>("BarData"); // Change this to your collection name
    }

    public IMongoCollection<PieDataModel> GetPieDataCollection()
    {
        return _database.GetCollection<PieDataModel>("PieData");
    }

    public IMongoCollection<GeographyDataModel> GetGeographyDataCollection()
    {
        return _database.GetCollection<GeographyDataModel>("GeographyData"); // Collection name can be changed as needed
    }
}
