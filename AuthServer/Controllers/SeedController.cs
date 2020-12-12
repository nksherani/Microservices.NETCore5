using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.ConfigStore;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeedController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<SeedController> _logger;
        private readonly ConfigurationStoreContext _dbContext;
        public SeedController(ILogger<SeedController> logger, ConfigurationStoreContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        
        [HttpGet]
        [Route("[action]")]
        public IActionResult Seed()
        {
            SeedAuthData.Seed(_dbContext);
            return Ok(true);
        }
    }
}
