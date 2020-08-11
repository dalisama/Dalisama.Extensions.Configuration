using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dalisama.Extensions.Configuration.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationProviderController : ControllerBase
    {
        

        private readonly ILogger<ConfigurationProviderController> _logger;

        public ConfigurationProviderController(ILogger<ConfigurationProviderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Dictionary<string,int> Get()
        {
            var rng = new Random();
            var dic = new Dictionary<string, int>();
            dic["Section1:Element1"] = rng.Next(150, 2000);
            dic["Section1:Element2"] = rng.Next(150, 2000);
            dic["Section1:Element3"] = rng.Next(150, 2000);
            dic["Section1:Element4"] = rng.Next(150, 2000);
            dic["Section2:Element1"] = rng.Next(150, 2000);
            dic["Section2:Element3"] = rng.Next(150, 2000);
            dic["Section2:Element4"] = rng.Next(150, 2000);
            dic["Section2:Element2"] = rng.Next(150, 2000);
            return dic;
        }
    }
}
