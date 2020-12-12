using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Cream", "Shampoo", "Cooking Oil", "Ice Cream"
        };

        private readonly IAppLogger _logger;

        public ProductsController(IAppLogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var methodName = "ProductsController.Get";
            _logger.Debug(methodName + " invoked");
            return Summaries;
        }
    }
}
