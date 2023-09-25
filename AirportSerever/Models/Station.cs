using AirportSerever.Enums;
using AirportSerever.Events;
using AirportSerever.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Xml.Linq;
using Switch = AirportSerever.Events.Switch;

namespace AirportSerever.Models
{
    public  class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string? Plane = null;
        private SemaphoreSlim _sem = new(1);
        private Switch _mSwitch;
        private readonly Queue<Station> landingQueue = new Queue<Station>();
        private readonly Queue<Station> departureQueue = new Queue<Station>();
        private readonly IHubContext<AirportHub> _airportHub;
        public int TimeInStaition { get; internal set; }

        public Station(int id)
        {
            Id = id;
           // _mSwitch = theSwitch;
            //_airportHub = airportHub;
            //_mSwitch.SwitchFlipped += this.OnSwitchFlipped;
        }




        public async Task<bool> Enter(string name, Direction direction, CancellationTokenSource cts)
        {
            try
            {
                
                cts.Token.ThrowIfCancellationRequested();
                Queue<Station> currentQueue = (direction == Direction.Departure) ? departureQueue : landingQueue;

                if (direction == Direction.Departure)
                    departureQueue.Enqueue(this);
                else
                    landingQueue.Enqueue(this);

                while (currentQueue.Count > 0 && currentQueue.Peek() != this)
                {
                    await Task.Delay(100);
                    cts.Token.ThrowIfCancellationRequested(); 
                }

                await _sem.WaitAsync(cts.Token);

                if (Plane != null)
                    Console.WriteLine("Crash !!!!!!!!");
                Plane = name;

              //  _ = _airportHub.Clients.All.SendAsync(Id.ToString(), $"{Plane}");
                return true;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Semaphore Enter Error Occurred: {ex.Message}");
                _sem.Release();
                return false;
            }
            finally
            {
                
                if (direction == Direction.Departure)
                    departureQueue.Dequeue();
                else
                    landingQueue.Dequeue();
            }
        }


        public void Exit()
        {
            //_ = _airportHub.Clients.All.SendAsync(Id.ToString(), $"{Plane}");
            Plane = null;
            _sem.Release();

        }


        public void OnSwitchFlipped(object sender, SwitchFlippedEventArgs args)
        {
            Console.WriteLine($"Station{Id}, switch flipped");
            if (args.State == SwitchState.On)
                Console.WriteLine("Turning on");
            else
                Console.WriteLine("Turning off");
        }

        public void RegToEvent(bool toReg)
        {
            if (toReg)
                _mSwitch.SwitchFlipped += OnSwitchFlipped;
            else 
                _mSwitch.SwitchFlipped -= OnSwitchFlipped;
        }
        public SwitchState GetSwitchState()
        {
            
            return _mSwitch.State;
        }
    }
}
