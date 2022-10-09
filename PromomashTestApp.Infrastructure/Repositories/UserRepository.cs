using Microsoft.EntityFrameworkCore;
using PromomashTestApp.Infrastructure.Entities;

namespace PromomashTestApp.Infrastructure.Repositories;

public interface IUserRepository
{
    public Task<bool> AnyAsync(string login, CancellationToken ct);
    
    public Task AddAsync(User user, CancellationToken ct);
}

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbDbContext)
    {
        _dbContext = dbDbContext;
    }

    /// <inheritdoc />
    public Task<bool> AnyAsync(string login, CancellationToken ct)
    {
        return _dbContext.Users.AnyAsync(x => x.Login == login, ct);
    }

    /// <inheritdoc />
    public Task AddAsync(User user, CancellationToken ct)
    {
        _dbContext.Users.Add(user);
        return _dbContext.SaveChangesAsync(ct);
    }
}
