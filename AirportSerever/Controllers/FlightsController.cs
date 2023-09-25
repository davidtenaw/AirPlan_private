using AirportSerever.BL;
using AirportSerever.Enums;
using AirportSerever.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirportSerever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly ControlTower Tower;
        private readonly IHubContext<AirportHub> _airportHub;

        public FlightsController(ControlTower tower, IHubContext<AirportHub> airportHub)
        {
            Tower = tower;
            _airportHub = airportHub;
        }


        // GET api/<FlightsController>/5
        [HttpGet("land/{plane}")]
        public string Land(string plane)
        {
            var msg = $"{plane}";
            Console.WriteLine($"    ({plane}) START [Landing]");
            Tower.AddFlight(msg, Direction.Landing);
            return msg;
        }
        [HttpGet("departure/{plane}")]
        public string Departure(string plane)
        {
            var msg = $"{plane}";
            Console.WriteLine($"    ({plane}) START [Departure]");
            Tower.AddFlight(msg, Direction.Departure);
            return msg;
        }
        [HttpGet("status")]
        public Status Status()
        {
            return Tower.GetStatus();
        }
     
    }
}
