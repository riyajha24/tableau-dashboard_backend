using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Models;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private static readonly IEnumerable<IndustryAnalyticsModel> AnalyticsData = new[]
        {
            new IndustryAnalyticsModel{Id = 1, Industry = "Technology", MarketShare = 25.4, GrowthRate = 3.2, Year = 2023 },
            new IndustryAnalyticsModel{Id = 2, Industry = "Healthcare", MarketShare = 18.7, GrowthRate = 4.1, Year = 2023 },
            new IndustryAnalyticsModel{Id = 3, Industry = "Finance", MarketShare = 15.3, GrowthRate = 2.8, Year = 2023 },
            new IndustryAnalyticsModel{Id = 4, Industry = "Energy", MarketShare = 10.9, GrowthRate = 1.7, Year = 2023 },
            new IndustryAnalyticsModel{Id = 5, Industry = "Retail", MarketShare = 12.2, GrowthRate = 3.5, Year = 2023 }
        };

        [HttpGet]
        public IEnumerable<IndustryAnalyticsModel> Get()
        {
            return AnalyticsData;
        }

        [HttpGet("{industry}")]
        public ActionResult<IndustryAnalyticsModel> Get(string industry)
        {
            var data = AnalyticsData.FirstOrDefault(a => a.Industry.Equals(industry, StringComparison.OrdinalIgnoreCase));
            if (data == null)
            {
                return NotFound();
            }
            return data;
        }
    }
}
