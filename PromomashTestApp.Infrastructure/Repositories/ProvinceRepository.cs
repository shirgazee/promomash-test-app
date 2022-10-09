using Microsoft.EntityFrameworkCore;
using PromomashTestApp.Infrastructure.Entities;

namespace PromomashTestApp.Infrastructure.Repositories;

public interface IProvinceRepository
{
    public Task<IEnumerable<Province>> GetProvincesAsync(Guid countryId, CancellationToken ct);
    
    public Task<bool> ExistsAsync(Guid id, Guid countryId, CancellationToken ct);
}

public class ProvinceRepository : IProvinceRepository
{
    private readonly AppDbContext _dbContext;

    public ProvinceRepository(AppDbContext dbDbContext)
    {
        _dbContext = dbDbContext;
    }

    public async Task<IEnumerable<Province>> GetProvincesAsync(Guid countryId, CancellationToken ct)
    {
        return await _dbContext.Provinces
            .Where(x => x.CountryId == countryId)
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
    }

    /// <inheritdoc />
    public Task<bool> ExistsAsync(Guid id, Guid countryId, CancellationToken ct)
    {
        return _dbContext.Provinces.AnyAsync(x => x.Id == id && x.Country.Id == countryId, ct);
    }
}
