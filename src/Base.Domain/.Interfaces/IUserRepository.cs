using Base.Domain.Entities;

namespace Base.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> Login(string email, string hash, CancellationToken cancellationToken);
    }
}
