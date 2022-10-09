using Microsoft.EntityFrameworkCore;
using PromomashTestApp.Infrastructure.Entities;

namespace PromomashTestApp.Infrastructure.Repositories;

public interface ICountryRepository
{
    public Task<IEnumerable<Country>> GetCountriesAsync(CancellationToken ct);

    public Task<bool> ExistsAsync(Guid id, CancellationToken ct);
}

public class CountryRepository : ICountryRepository
{
    private readonly AppDbContext _dbContext;

    public CountryRepository(AppDbContext dbDbContext)
    {
        _dbContext = dbDbContext;
    }

    public async Task<IEnumerable<Country>> GetCountriesAsync(CancellationToken ct)
    {
        return await _dbContext.Countries.OrderBy(x => x.Name).ToListAsync(ct);
    }

    /// <inheritdoc />
    public Task<bool> ExistsAsync(Guid id, CancellationToken ct)
    {
        return _dbContext.Countries.AnyAsync(x => x.Id == id, ct);
    }
}
