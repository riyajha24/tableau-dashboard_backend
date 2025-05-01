using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ReactApp1.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IMongoCollection<InvoiceModel> _invoiceCollection;

        public InvoicesController(MongoDbService mongoDbService)
        {
            _invoiceCollection = mongoDbService.GetInvoiceCollection();
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<IEnumerable<InvoiceModel>> Get()
        {
            return await _invoiceCollection.Find(_ => true).ToListAsync();
        }

        // GET: api/Invoices/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceModel>> Get(string id)
        {
            var invoice = await _invoiceCollection.Find(i => i.Id == id).FirstOrDefaultAsync();
            if (invoice == null)
            {
                return NotFound();
            }
            return invoice;
        }

        // POST: api/Invoices
        [HttpPost]
        public async Task<ActionResult> Create(InvoiceModel newInvoice)
        {
            // Ensure that the Id is not set, so MongoDB can generate it
            newInvoice.Id = null;

            await _invoiceCollection.InsertOneAsync(newInvoice);
            return CreatedAtAction(nameof(Get), new { id = newInvoice.Id }, newInvoice);
        }

        // Additional methods (PUT, DELETE, etc.) can be added here as needed
    }
}
