using Base.Domain.Entities;

namespace Base.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<Customer?> GetCustomerByIdUser(long userId, CancellationToken cancellationToken);
        public Task<Customer?> GetCustomerById(long id, CancellationToken cancellationToken);
    }
}
