using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ReactApp1.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IMongoCollection<TransactionModel> _transactionCollection;

        public TransactionController(MongoDbService mongoDbService)
        {
            _transactionCollection = mongoDbService.GetTransactionCollection();
        }

        // GET: api/Transaction
        [HttpGet]
        public async Task<IEnumerable<TransactionModel>> Get()
        {
            return await _transactionCollection.Find(_ => true).ToListAsync();
        }

        // GET: api/Transaction/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionModel>> Get(string id)
        {
            var transaction = await _transactionCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
            if (transaction == null)
            {
                return NotFound();
            }
            return transaction;
        }

        // POST: api/Transaction
        [HttpPost]
        public async Task<ActionResult> Create(TransactionModel newTransaction)
        {
            newTransaction.Id = null; // Ensure Id is null to let MongoDB generate it
            await _transactionCollection.InsertOneAsync(newTransaction);
            return CreatedAtAction(nameof(Get), new { id = newTransaction.Id }, newTransaction);
        }
    }
}
