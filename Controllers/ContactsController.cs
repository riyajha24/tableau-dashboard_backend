using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ReactApp1.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IMongoCollection<ContactModel> _contactCollection;

        public ContactsController(MongoDbService mongoDbService)
        {
            _contactCollection = mongoDbService.GetContactCollection();
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<IEnumerable<ContactModel>> Get()
        {
            return await _contactCollection.Find(_ => true).ToListAsync();
        }

        // GET: api/Contacts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactModel>> Get(string id)
        {
            var contact = await _contactCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (contact == null)
            {
                return NotFound();
            }
            return contact;
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<ActionResult> Create(ContactModel newContact)
        {
            // Ensure that the Id is not set, so MongoDB can generate it
            newContact.Id = null;

            await _contactCollection.InsertOneAsync(newContact);
            return CreatedAtAction(nameof(Get), new { id = newContact.Id }, newContact);
        }
    }
}
