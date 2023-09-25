using AirportSerever.Enums;
using AirportSerever.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace AirportSerever.Data
{
    public class AirportContext : DbContext
    {
        public AirportContext(DbContextOptions<AirportContext> options) : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Graph> Graphs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create instances and populate data for Station entities
            var stations = new Station[10]; // Create an array to hold the stations

            for (int i = 0; i < 10; i++)
            {
                stations[i] = new Station(i + 1); // Create and initialize each Station object
            }

            // Create instances and populate data for Graph entities
            var landingRoute = new Graph();
            var departureRoute = new Graph();

            // Create relationships between stations and routes for LandingRoute
            landingRoute.AddEdge(stations[0], stations[1]);
            landingRoute.AddEdge(stations[1], stations[2]);
            landingRoute.AddEdge(stations[2], stations[3]);
            landingRoute.AddEdge(stations[3], stations[4]);
            landingRoute.AddEdge(stations[4], stations[5]);
            landingRoute.AddEdge(stations[5], stations[6]);
            landingRoute.AddEdge(stations[5], stations[7]);

            // Create relationships between stations and routes for DepartureRoute
            departureRoute.AddEdge(stations[0], stations[6]);
            departureRoute.AddEdge(stations[0], stations[7]);
            departureRoute.AddEdge(stations[6], stations[8]);
            departureRoute.AddEdge(stations[7], stations[8]);
            departureRoute.AddEdge(stations[8], stations[4]);
            departureRoute.AddEdge(stations[4], stations[9]);

            // Use HasData to add the instances to the respective tables
            modelBuilder.Entity<Station>().HasData(stations);
            modelBuilder.Entity<Graph>().HasData(landingRoute, departureRoute);
        }


    }
}
