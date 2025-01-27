using Base.Domain.Shared;
using MediatR;

namespace Base.Application.Wallet.Queries.GetWalletByCustomerId;

public record GetWalletByCustomerIdRequest(long Id) : IRequest<Result<GetWalletByCustomerIdResponse>> { }

