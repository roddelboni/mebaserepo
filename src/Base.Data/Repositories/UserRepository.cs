using Base.Data.Context;
using Base.Domain.Entities;
using Base.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Base.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly BaseContext _context;

    public UserRepository(BaseContext context) : base(context) => _context = context;

    public async Task<User?> Login(string email, string hash, CancellationToken cancellationToken)
        => await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == hash, cancellationToken);
}
