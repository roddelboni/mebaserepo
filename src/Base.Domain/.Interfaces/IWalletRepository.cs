using Base.Domain.Entities;

namespace Base.Domain.Interfaces
{
    public interface IWalletRepository
    {
        public Task<decimal?> GetBalanceByIdCustomer(long customerId, CancellationToken cancellationToken);
        public Task<Wallet?> GetWalletByIdCustomer(long customerId, CancellationToken cancellationToken);
    }
}
