using AirportSerever.Models;
using AirportSerever.Repository;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace AirportSerever.Services
{
    public class Service
    {
        public Service(IRepository<Flight> flightRepo, IRepository<Station> stationRepo)
        {
            FlightRepo = flightRepo;
            StationRepo = stationRepo;


        }

        public IRepository<Flight> FlightRepo { get; }
        public IRepository<Station> StationRepo { get; }

    }
}
