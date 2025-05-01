using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ReactApp1.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PieDataController : ControllerBase
    {
        private readonly IMongoCollection<PieDataModel> _pieDataCollection;

        public PieDataController(MongoDbService mongoDbService)
        {
            _pieDataCollection = mongoDbService.GetPieDataCollection();
        }

        // GET: api/piedata
        [HttpGet]
        public async Task<ActionResult<List<PieDataModel>>> GetAll()
        {
            var pieData = await _pieDataCollection.Find(_ => true).ToListAsync();

            // Create a new list with Id as a string
            var response = pieData.Select(item => new
            {
                Id = item.Id.ToString(), // Convert ObjectId to string
                item.Label,
                item.Value,
                item.Color
            }).ToList();

            return Ok(response);
        }

        // GET: api/piedata/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PieDataModel>> GetById(string id)
        {
            var pieData = await _pieDataCollection.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();
            if (pieData == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                Id = pieData.Id.ToString(), // Convert ObjectId to string
                pieData.Label,
                pieData.Value,
                pieData.Color
            });
        }

        // POST: api/piedata
        [HttpPost]
        public async Task<ActionResult<PieDataModel>> Create(PieDataModel newPieData)
        {
            await _pieDataCollection.InsertOneAsync(newPieData);
            return CreatedAtAction(nameof(GetById), new { id = newPieData.Id.ToString() }, newPieData);
        }
    }
}
