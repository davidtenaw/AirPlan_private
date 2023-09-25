using AirportSerever.Data;
using AirportSerever.Enums;
using AirportSerever.Events;
using AirportSerever.Hubs;
using AirportSerever.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AirportSerever.Repository
{
    public class StationRepository : IStation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Plane { get; set; }
        public SemaphoreSlim Semaphore { get; }
        public Switch Switch { get; set; }
        public Queue<Station> LandingQueue { get; }
        public Queue<Station> DepartureQueue { get; }

        public IHubContext<AirportHub> AirportHub { get; }

        public int TimeInStation { get; }
        IHubContext<AirportHub> IStation.AirportHub { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private readonly AirportContext _context;
        public StationRepository(AirportContext context)
        {
            _context = context;
            TimeInStation = 1000;

        }

        public async Task<bool> Enter(string name, Direction direction, CancellationTokenSource cts)
        {
            try
            {
                cts.Token.ThrowIfCancellationRequested();
                Queue<Station> currentQueue = (direction == Direction.Departure) ? DepartureQueue : LandingQueue;

                // Fetch the first station from the database (or your data store)
                var firstStation = _context.Stations.First();

                if (direction == Direction.Departure)
                    DepartureQueue.Enqueue(firstStation);
                else
                    LandingQueue.Enqueue(firstStation);

                while (currentQueue.Count > 0 && currentQueue.Peek() != firstStation)
                {
                    await Task.Delay(100);
                    cts.Token.ThrowIfCancellationRequested();
                }

                await Semaphore.WaitAsync(cts.Token);

                if (firstStation.Plane != null)
                    Console.WriteLine("Crash !!!!!!!!");
                firstStation.Plane = name;

                // Implement your logic for sending data via AirportHub here

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Semaphore Enter Error Occurred: {ex.Message}");
                Semaphore.Release();
                return false;
            }
            finally
            {
                if (direction == Direction.Departure)
                    DepartureQueue.Dequeue();
                else
                    LandingQueue.Dequeue();
            }
        }


        public void Exit()
        {
            throw new NotImplementedException();
        }

        public Station? GetStationByID(int stationId)
        {
            // Implement the logic to fetch a station by its ID from your data store (e.g., using Entity Framework Core)
            return _context.Stations.FirstOrDefault(station => station.Id == stationId);
        }

        public SwitchState GetSwitchState()
        {
            throw new NotImplementedException();
        }

        public void OnSwitchFlipped(object sender, SwitchFlippedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void RegToEvent(bool toReg)
        {
            throw new NotImplementedException();
        }
    }
}
