using Base.Domain.Shared;
using MediatR;

namespace Base.Application.Wallet.Commands.AddBalance;

public record AddBalanceRequest(long UserId, decimal Balance):IRequest<Result<AddBalanceResponse>>;
