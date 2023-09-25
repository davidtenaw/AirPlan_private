using AirportSerever.Enums;
using AirportSerever.Events;
using AirportSerever.Hubs;
using AirportSerever.Models;
using Microsoft.AspNetCore.SignalR;
using System.Data.SqlTypes;

namespace AirportSerever.BL
{
    public class Runway
    {
        private Switch switch1 = new Switch();

        Station[] s = new Station[10];

        public Runway()
        {
           
            for (int i = 0; i < 10; i++)
            {
                s[i] = new Station(i);
            }
        }

        public Graph GetRoute(Direction direction, int id = 0)
        {
            switch (direction)
            {
                case Direction.Landing:
                    {
                        var route = LandingRoute();
                        return route;
                    }

                case Direction.Departure:
                    {
                        var route = DepartureRoute();
                        return route;
                    }

                default:
                    throw new Exception();
            }
        }

        private Graph LandingRoute()
        {
            var g = new Graph();
            g.AddEdge(s[0], s[1]);
            g.AddEdge(s[1], s[2]);
            g.AddEdge(s[2], s[3]);
            g.AddEdge(s[3], s[4]);
            g.AddEdge(s[4], s[5]);
            g.AddEdge(s[5], s[6]);
            g.AddEdge(s[5], s[7]);
            return g;
        }

        private Graph DepartureRoute()
        {
            var g = new Graph();
            g.AddEdge(s[0], s[6]);
            g.AddEdge(s[0], s[7]);
            g.AddEdge(s[6], s[8]);
            g.AddEdge(s[7], s[8]);
            g.AddEdge(s[8], s[4]);
            g.AddEdge(s[4], s[9]);

            return g;
        }
    }

}