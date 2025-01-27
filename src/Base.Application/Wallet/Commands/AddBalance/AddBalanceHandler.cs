using Base.Data.Context;
using Base.Domain.Entities;
using Base.Domain.Interfaces;
using Base.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Base.Application.Wallet.Commands.AddBalance;

public class AddBalanceHandler : IRequestHandler<AddBalanceRequest, Result<AddBalanceResponse>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly BaseContext _baseContext;
    private readonly ILogger<AddBalanceHandler> _logger;

    public AddBalanceHandler(IWalletRepository walletRepository, ICustomerRepository customerRepository, BaseContext baseContext, ILogger<AddBalanceHandler> logger) 
    { _walletRepository = walletRepository; _customerRepository = customerRepository; _baseContext = baseContext; _logger = logger; }

    public async Task<Result<AddBalanceResponse>> Handle(AddBalanceRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByIdUser(request.UserId, cancellationToken);

        if (customer == null)
        {
            return Result<AddBalanceResponse>.Failure(Error.BigError);
        }

        var walletBalance = await _walletRepository.GetWalletByIdCustomer(customer.Id, cancellationToken);

        if (walletBalance == null)
        {
            return Result<AddBalanceResponse>.Failure(Error.BigError);
        }

        var newBalance = request.Balance + walletBalance.Balance;

        var newWallet = new Domain.Entities.Wallet(walletBalance.WalletNumber, newBalance);

        var transfer = new Blockchain(newBalance, DateTime.UtcNow, customer.Id, null);

        try
        {
            walletBalance.Update(newWallet);
            _baseContext.Wallets.Update(walletBalance);
            await _baseContext.AddAsync(transfer);
            var affected = await _baseContext.SaveChangesAsync(cancellationToken);

            return Result<AddBalanceResponse>.Sucess(new AddBalanceResponse { Name = customer.Name, Balance = walletBalance.Balance }, CommandResultStatus.Ok);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}{ex}", ex.Message, ex);
            throw;
        }
    }
}
