using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ReactApp1.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LineDataController : ControllerBase
    {
        private readonly IMongoCollection<LineData> _lineDataCollection;

        public LineDataController(MongoDbService mongoDbService)
        {
            _lineDataCollection = mongoDbService.GetLineDataCollection();
        }

        // GET: api/LineData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineData>>> Get()
        {
            if (HttpContext.Items["Team"] is TeamModel team)
            {
                // Log the access level
                Console.WriteLine($"Access Level for {team.Email}: {team.Access}");

                if (team.Access.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    var lineData = await _lineDataCollection.Find(_ => true).ToListAsync();
                    return Ok(lineData);
                }
            }

            return Unauthorized(); // Return 401 Unauthorized if access is denied
        }

        // GET: api/LineData/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LineData>> Get(string id)
        {
            if (HttpContext.Items["Team"] is TeamModel team)
            {
                // Log the access level
                Console.WriteLine($"Access Level for {team.Email}: {team.Access}");

                if (team.Access.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    var lineData = await _lineDataCollection.Find(ld => ld.Id == id).FirstOrDefaultAsync();
                    if (lineData == null)
                    {
                        return NotFound();
                    }
                    return Ok(lineData);
                }
            }

            return Unauthorized(); // Return 401 Unauthorized if access is denied
        }

        // POST: api/LineData
        [HttpPost]
        public async Task<ActionResult> Create(LineData newLineData)
        {
            if (HttpContext.Items["Team"] is TeamModel team)
            {
                // Log the access level
                Console.WriteLine($"Access Level for {team.Email}: {team.Access}");

                if (team.Access.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    await _lineDataCollection.InsertOneAsync(newLineData);
                    return CreatedAtAction(nameof(Get), new { id = newLineData.Id }, newLineData);
                }
            }

            return Unauthorized(); // Return 401 Unauthorized if access is denied
        }

        // Additional methods (PUT, DELETE, etc.) can be added here as needed
    }
}
