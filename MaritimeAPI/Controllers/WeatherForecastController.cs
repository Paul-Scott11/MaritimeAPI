using MaritimeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaritimeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly MaritimeTechTestContext _context;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, MaritimeTechTestContext context)
        {
            _logger = logger;
            _context = context;
        }              

        [HttpGet]
        [Route("api/EntryReport")]
        public IEnumerable<TblRandomNumber> GetFromDB()
        {
            return _context.TblRandomNumbers.ToArray();          
        }

   
    }
}
