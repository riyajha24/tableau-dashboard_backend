using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ReactApp1.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly IMongoCollection<TeamModel> _teamCollection;

        public TeamController(MongoDbService mongoDbService)
        {
            _teamCollection = mongoDbService.GetTeamCollection(); // Updated to use GetTeamCollection
        }

        // GET: api/Team
        [HttpGet]
        public async Task<IEnumerable<TeamModel>> Get()
        {
            return await _teamCollection.Find(_ => true).ToListAsync();
        }

        // GET: api/Team/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamModel>> Get(string id)
        {
            var teamMember = await _teamCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
            if (teamMember == null)
            {
                return NotFound();
            }
            return teamMember;
        }

        // POST: api/Team
        [HttpPost]
        public async Task<ActionResult> Create(TeamModel newTeamMember)
        {
            newTeamMember.Id = null; // Ensure Id is null to let MongoDB generate it
            await _teamCollection.InsertOneAsync(newTeamMember);
            return CreatedAtAction(nameof(Get), new { id = newTeamMember.Id }, newTeamMember);
        }
    }
}
