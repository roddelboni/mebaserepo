using Base.Data.Auth;
using Base.Data.Context;
using Base.Domain.Entities;
using Base.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Base.Application.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, Result<CreateUserResponse>>
    {
        private readonly BaseContext _database;
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly IAuthService _authService;

        public CreateUserHandler(BaseContext baseContext, ILogger<CreateUserHandler> logger, IAuthService authService) { 
            _database = baseContext;
            _logger = logger;
            _authService = authService;
        }


        public async Task<Result<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var hash = _authService.ComputeHash(request.Password);

            var user = new User(request.Email, true, hash, request.Role);

            var customer = new Customer(request.FullName, request.Document, request.BirthDate);

            var wallet = new Domain.Entities.Wallet(Guid.NewGuid(), 0);

            customer.AddWallet(wallet);

            user.AddCustomer(customer);

            try
            {

                await _database.AddAsync(user);
                await _database.SaveChangesAsync();

                return Result<CreateUserResponse>.Sucess(user.CreateUserToResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}{ex}", ex.Message, ex);
                throw;
            }
        }
    }
}
