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
    public class GeographyDataController : ControllerBase
    {
        private readonly IMongoCollection<GeographyDataModel> _geographyDataCollection;

        public GeographyDataController(MongoDbService mongoDbService)
        {
            _geographyDataCollection = mongoDbService.GetGeographyDataCollection();
        }

        // GET: api/geographydata
        [HttpGet]
        public async Task<ActionResult<List<GeographyDataModel>>> GetAll()
        {
            if (HttpContext.Items["Team"] is TeamModel team)
            {
                // Log the access level
                Console.WriteLine($"Access Level for {team.Email}: {team.Access}");

                if (team.Access.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    var geographyData = await _geographyDataCollection.Find(_ => true).ToListAsync();
                    return Ok(geographyData.Select(item => new
                    {
                        Id = item.Id.ToString(), // Return as string for API consumers
                        item.IdCode,
                        item.Value
                    }).ToList());
                }
            }

            return Unauthorized(); // Return 401 Unauthorized if access is denied
        }

        // GET: api/geographydata/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GeographyDataModel>> GetById(string id)
        {
            if (HttpContext.Items["Team"] is TeamModel team)
            {
                // Log the access level
                Console.WriteLine($"Access Level for {team.Email}: {team.Access}");

                if (team.Access.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    var geographyData = await _geographyDataCollection.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();
                    if (geographyData == null)
                    {
                        return NotFound();
                    }
                    return Ok(new
                    {
                        Id = geographyData.Id.ToString(), // Return as string for API consumers
                        geographyData.IdCode,
                        geographyData.Value
                    });
                }
            }

            return Unauthorized(); // Return 401 Unauthorized if access is denied
        }

        // POST: api/geographydata
        [HttpPost]
        public async Task<ActionResult<GeographyDataModel>> Create(GeographyDataModel newGeographyData)
        {
            if (HttpContext.Items["Team"] is TeamModel team)
            {
                // Log the access level
                Console.WriteLine($"Access Level for {team.Email}: {team.Access}");

                if (team.Access.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    await _geographyDataCollection.InsertOneAsync(newGeographyData);
                    return CreatedAtAction(nameof(GetById), new { id = newGeographyData.Id.ToString() }, newGeographyData);
                }
            }

            return Unauthorized(); // Return 401 Unauthorized if access is denied
        }
    }
}
