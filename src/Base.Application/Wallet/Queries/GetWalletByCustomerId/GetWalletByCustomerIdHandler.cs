using Base.Domain.Interfaces;
using Base.Domain.Shared;
using MediatR;

namespace Base.Application.Wallet.Queries.GetWalletByCustomerId
{
    internal class GetWalletByCustomerIdHandler : IRequestHandler<GetWalletByCustomerIdRequest, Result<GetWalletByCustomerIdResponse>>
    {
        private readonly IWalletRepository _walletRepository;
        public GetWalletByCustomerIdHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<Result<GetWalletByCustomerIdResponse>> Handle(GetWalletByCustomerIdRequest request, CancellationToken cancellationToken)
        {
            var balance = await _walletRepository.GetBalanceByIdCustomer(request.Id, cancellationToken);

            var result = new GetWalletByCustomerIdResponse(Balance: balance.Value);

            return balance is null
                ? Result<GetWalletByCustomerIdResponse>.Failure(Error.None)
                : Result<GetWalletByCustomerIdResponse>.Sucess(result);    
        }
    }
}
