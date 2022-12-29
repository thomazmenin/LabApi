using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LabApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PingController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public PingController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var ret = new { 
                    pong = true,
                    timestamp = DateTime.Now.ToString(),
                    machine = Environment.MachineName
                };  

            return Ok(ret);
        }
    }
}