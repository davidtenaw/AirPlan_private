using AirportSerever.Models;
using AirportSerever.Repository;

internal class GraphRepository : IRepository<Station>
{
    public Task AddAsync(Station entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string Name = null, int id = 0)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Station>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Station> GetAsync(string Name = null, int id = 0)
    {
        throw new NotImplementedException();
    }

    public Task Save()
    {
        throw new NotImplementedException();
    }
}