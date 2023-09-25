using AirportSerever.Models;

namespace AirportSerever.Repository
{
    public interface IRepository<T>
    {
        Task AddAsync(T entity);
        Task DeleteAsync(string Name = null, int id = 0);
        
        Task<IEnumerable<T>> GetAllAsync();
        Task Save();
        Task<T> GetAsync(string Name = null, int id = 0);

        //Task AddAsync(Flight entity);
        //Task DeleteAsync(string name = null, int id = 0);
        //Task<IEnumerable<Flight>> GetAllAsync();
        //Task SaveAsync();
        //Task<Flight> GetAsync(string name = null, int id = 0);

        //// Additional methods from the Flight class
        //void Run();
        //Task<Station?> MoveToStation(Station nextStation, CancellationTokenSource cts);
    }
}