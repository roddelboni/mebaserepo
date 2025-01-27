using Base.Data.Context;
using Base.Domain.Entities;
using Base.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Base.Data.Repositories
{
    internal class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly BaseContext _context;

        public CustomerRepository(BaseContext context) : base(context) => _context = context;

        public async Task<Customer?> GetCustomerByIdUser(long userId, CancellationToken cancellationToken)
        => await _context.Customers.Include(c => c.User)
                .Where(x => x.User.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<Customer?> GetCustomerById(long id, CancellationToken cancellationToken)
        => await _context.Customers
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
