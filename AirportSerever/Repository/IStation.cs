using AirportSerever.Enums;
using AirportSerever.Events;
using AirportSerever.Hubs;
using AirportSerever.Models;
using Microsoft.AspNetCore.SignalR;

namespace AirportSerever.Repository
{
    public interface IStation
    {
        int Id { get; set; }
        string Name { get; set; }
        string? Plane { get; set; }
        SemaphoreSlim Semaphore { get; }
        Switch Switch { get; set; }
        Queue<Station> LandingQueue { get; }
        Queue<Station> DepartureQueue { get; }
        IHubContext<AirportHub> AirportHub { get; set; }
        int TimeInStation { get; }

        Task<bool> Enter(string name, Direction direction, CancellationTokenSource cts);
        void Exit();
        Station? GetStationByID(int stationId);
        void OnSwitchFlipped(object sender, SwitchFlippedEventArgs args);
        void RegToEvent(bool toReg);
        SwitchState GetSwitchState();
    }
}
