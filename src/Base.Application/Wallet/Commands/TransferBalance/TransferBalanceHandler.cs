using Base.Application.Wallet.Commands.AddBalance;
using Base.Data.Context;
using Base.Domain.Entities;
using Base.Domain.Interfaces;
using Base.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Base.Application.Wallet.Commands.TransferBalance
{
    public class TransferBalanceHandler : IRequestHandler<TransferBalanceRequest, Result<TransferBalanceResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly BaseContext _baseContext;
        private readonly ILogger<AddBalanceHandler> _logger;

        public TransferBalanceHandler(IWalletRepository walletRepository, ICustomerRepository customerRepository, BaseContext baseContext, ILogger<AddBalanceHandler> logger)
        { _walletRepository = walletRepository; _customerRepository = customerRepository; _baseContext = baseContext; _logger = logger; }

        public async Task<Result<TransferBalanceResponse>> Handle(TransferBalanceRequest request, CancellationToken cancellationToken)
        {

            var customerOut = await _customerRepository.GetCustomerById(request.CustomerIdOut, cancellationToken);
            var customerIn= await _customerRepository.GetCustomerById(request.CustomerIdIn, cancellationToken);


            var walletCustomerOut = await _walletRepository.GetWalletByIdCustomer(request.CustomerIdOut, cancellationToken);
            var walletCustomerIn = await _walletRepository.GetWalletByIdCustomer(request.CustomerIdIn, cancellationToken);

            if (walletCustomerOut == null || walletCustomerIn == null)
            {
                return Result<TransferBalanceResponse>.Failure(Error.BigError);
            }

            var newBalanceOut = walletCustomerOut.Balance - request.Value;
            var newBalanceIn = walletCustomerIn.Balance + request.Value;

            var newWalletOut = new Domain.Entities.Wallet(walletCustomerOut.WalletNumber, newBalanceOut);
            var newWalletIn = new Domain.Entities.Wallet(walletCustomerOut.WalletNumber, newBalanceIn);

            var transferOut = new Blockchain(request.Value * -1, DateTime.UtcNow, request.CustomerIdOut, request.CustomerIdIn);
            var transferIn = new Blockchain(request.Value, DateTime.UtcNow, request.CustomerIdIn, request.CustomerIdOut);

            try
            {
                walletCustomerOut.Update(newWalletOut);
                walletCustomerIn.Update(newWalletIn);

                _baseContext.Wallets.Update(walletCustomerOut);
                _baseContext.Wallets.Update(walletCustomerIn);

                await _baseContext.AddAsync(transferOut);
                await _baseContext.AddAsync(transferIn);

                var affected = await _baseContext.SaveChangesAsync(cancellationToken);

                return Result<TransferBalanceResponse>.Sucess(new TransferBalanceResponse 
                {        NameIn = customerIn.Name,
                        NameOut = customerOut.Name,
                        BalanceIn = newBalanceIn,
                        BalanceOut = newBalanceOut
                }, CommandResultStatus.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}{ex}", ex.Message, ex);
                throw;
            }


            throw new NotImplementedException();
        }
    }
}
