using AirportSerever.Data;
using AirportSerever.Models;
using System.Xml.Linq;

namespace AirportSerever.Repository
{
    public class FlightRepository : IRepository<Flight>
    {
        private readonly AirportContext context;

        public FlightRepository(AirportContext context)
        {
            this.context = context;
        }



        public Flight GetById(int id) => context.Flights.FirstOrDefault(a => a.Id == id);

        public Flight GetByName(string name) => context.Flights.FirstOrDefault(a => a.Name == name);

        public Task<IEnumerable<Flight>> GetAllAsync() => Task.FromResult<IEnumerable<Flight>>(context.Flights);


        public async Task<Flight> GetAsync(string flightName = null, int id = 0)
        {
            if (flightName != null)
            {
                return await Task.Run(() => GetByName(flightName));
            }
            else
            {
                return await Task.Run(() => GetById(id));
            }
        }


        public async Task AddAsync(Flight entity)
        {
            context.Flights.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string flightName, int id)
        {
            if (flightName != null)
            {
                var flightToDelete = GetByName(flightName);
                if (flightToDelete != null)
                {
                    context.Flights.Remove(flightToDelete);
                    await context.SaveChangesAsync();
                }
            }
            else
            {
                var flightToDelete = GetById(id);
                if (flightToDelete != null)
                {
                    context.Flights.Remove(flightToDelete);
                    await context.SaveChangesAsync();
                }
            }
        }

   
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }


    }
}
