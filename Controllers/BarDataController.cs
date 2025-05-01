using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ReactApp1.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarDataController : ControllerBase
    {
        private readonly IMongoCollection<BarDataModel> _barDataCollection;

        public BarDataController(MongoDbService mongoDbService)
        {
            _barDataCollection = mongoDbService.GetBarDataCollection();
        }

        // GET: api/bardata
        [HttpGet]
        public async Task<ActionResult<List<BarDataModel>>> GetAll()
        {
            if (HttpContext.Items["Team"] is TeamModel team)
            {
                // Log the access level
                Console.WriteLine($"Access Level for {team.Email}: {team.Access}");

                if (team.Access.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    var barData = await _barDataCollection.Find(_ => true).ToListAsync();
                    return Ok(barData);
                }
            }

            return Unauthorized(); // Return 401 Unauthorized if access is denied
        }

        // GET: api/bardata/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BarDataModel>> GetById(string id)
        {
            if (HttpContext.Items["Team"] is TeamModel team)
            {
                // Log the access level
                Console.WriteLine($"Access Level for {team.Email}: {team.Access}");

                if (team.Access.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    var barData = await _barDataCollection.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();
                    if (barData == null)
                    {
                        return NotFound();
                    }
                    return Ok(barData);
                }
            }

            return Unauthorized(); // Return 401 Unauthorized if access is denied
        }

        // POST: api/bardata
        [HttpPost]
        public async Task<ActionResult<BarDataModel>> Create(BarDataModel newBarData)
        {
            if (HttpContext.Items["Team"] is TeamModel team)
            {
                // Log the access level
                Console.WriteLine($"Access Level for {team.Email}: {team.Access}");

                if (team.Access.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    await _barDataCollection.InsertOneAsync(newBarData);
                    return CreatedAtAction(nameof(GetById), new { id = newBarData.Id.ToString() }, newBarData);
                }
            }

            return Unauthorized(); // Return 401 Unauthorized if access is denied
        }
    }
}
