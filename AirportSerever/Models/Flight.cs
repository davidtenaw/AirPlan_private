using AirportSerever.BL;
using AirportSerever.Enums;
using AirportSerever.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Numerics;
using System.Xml.Linq;

namespace AirportSerever.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string Name;
        public int StationId = 0;
        public FlightRunway Runway;
        

        public readonly IHubContext<AirportHub> _airportHub;

        public Flight(string flightName, FlightRunway route, IHubContext<AirportHub> airportHub)
        {
            Name = flightName;
            Runway = route;
            _airportHub = airportHub;
            Run();
        }
        internal void Run()
        {
            Task.Run(async () =>
            {


                Station? currStation = null;
                var nextStations = Runway.GetNextStations(currStation);
                var cts = new CancellationTokenSource();
                while (nextStations.Count > 0)
                {
                    

                    var nextStation = await Runway.GetNextStationAsync(currStation);
                    if (nextStation == null)
                        break;

                    StationId = nextStation.Id; 
                    currStation = nextStation;
                    var tasks = nextStations.Select(S => EnterStationTask(S, cts)).ToArray();
                    var task = Task.WaitAny(tasks);
                    currStation = tasks[task].Result;
                    

                    nextStations = Runway.GetNextStations(currStation);
                    Console.WriteLine($"[{Runway.Direction.ToString()}] {Name} ==> s{StationId}");
                    
                    

                }
                Console.WriteLine($"    ({Name}) FINISH [{Runway.Direction.ToString()}]");
                currStation!.Exit();
                currStation.Plane = null;
                _ = _airportHub.Clients.All.SendAsync(currStation.Id.ToString(), $"{Name}");
            });

        }


        internal async Task<Station?> MoveToStation(Station nextStation, CancellationTokenSource cts)
        {
            await nextStation.Enter(Name ,Runway.Direction, cts);
            return nextStation;
        }

        private Task<Station> EnterStationTask(Station nextStation, CancellationTokenSource cts)
        {
            return Task.Run(() => MoveToStation(nextStation, cts))!;
        }

    }
}
