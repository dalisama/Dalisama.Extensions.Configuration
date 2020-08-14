using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dalisama.Extensions.Configuration.Consumer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfConsumerController : ControllerBase
    {


        private readonly ILogger<ConfConsumerController> _logger;

        public ConfConsumerController(ILogger<ConfConsumerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<ClassOption> Get([FromServices] IOptionsSnapshot<ClassOption> option1, [FromServices] IOptions<ClassOption> option2)
        {
            return new List<ClassOption> { option1.Value, option2.Value};
        }
    }
}
