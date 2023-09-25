using AirportSerever.Data;
using AirportSerever.Enums;
using AirportSerever.Hubs;
using AirportSerever.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportSerever.BL
{
    public class FlightRunway
    {
        //public Graph Runway;
        
        public Direction Direction { get; }

        public Runway Runway;

        public FlightRunway(Direction direction)
        {
            Runway = new Runway();
            Direction = direction;
        }



        public async Task<Station?> GetNextStationAsync(Station? station)
        {
            return await Task.FromResult(Runway.GetRoute(Direction).GetNext(station).FirstOrDefault());
        }

        public List<Station> GetNextStations(Station? station)
        {
            return Runway.GetRoute(Direction).GetNext(station);
        }


        public Station? GetStationByID(int stationId)
        {
            return Runway.GetRoute(Direction).Nodes.FirstOrDefault(station => station.Id == stationId);
        }


        public void TerminalStatus()
        {
            // Implement terminal status logic here
        }
    }
}
