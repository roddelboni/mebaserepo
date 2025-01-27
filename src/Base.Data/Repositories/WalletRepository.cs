using Base.Data.Context;
using Base.Domain.Entities;
using Base.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Base.Data.Repositories;

public class WalletRepository : Repository<Wallet>, IWalletRepository
{
    private readonly BaseContext _context;

    public WalletRepository(BaseContext context) : base(context) => _context = context;

    public async Task<decimal?> GetBalanceByIdCustomer(long customerId, CancellationToken cancellationToken)
        => await _context.Customers
                .Where(x => x.Id == customerId)
                .Select(c => c.CustomerWallet.Balance)                
                .FirstOrDefaultAsync(cancellationToken);

    public async Task<Wallet?> GetWalletByIdCustomer(long customerId, CancellationToken cancellationToken)
    => await _context.Wallets           
            .Where(x => x.CustomerId == customerId)
            .FirstOrDefaultAsync(cancellationToken);

}
