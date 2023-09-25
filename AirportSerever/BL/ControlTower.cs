using AirportSerever.Data;
using AirportSerever.Enums;
using AirportSerever.Hubs;
using AirportSerever.Models;
using Microsoft.AspNetCore.SignalR;
using System.Xml.Serialization;

namespace AirportSerever.BL
{
    public class ControlTower
    {
        List<Flight> _flights = new();
        private readonly Runway _runway;
        private readonly IHubContext<AirportHub> airportHub;
        private readonly AirportContext _context;

        public ControlTower(Runway routesBuilder, IHubContext<AirportHub> airportHub)
        {
            _runway = routesBuilder;
            this.airportHub = airportHub;
            //this._context = airportContext;
        }

        public void AddFlight(string flightName, Direction direction)
        {
            _flights.Add(new Flight(flightName, new FlightRunway(direction), airportHub));
        }


        public void RemoveFlight(string flightName)
        {
            var flight = _flights.FirstOrDefault(f => f.Name == flightName);
            _flights.Remove(flight);
        }

        public Status GetStatus()
        {
            var list = _flights.Select(f => $"{f.Name} is Attribute {f.StationId}").ToList();
            return new Status { Flights = list };
        }


    }
}