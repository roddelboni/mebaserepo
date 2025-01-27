using Base.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Wallet.Commands.TransferBalance
{
    public class TransferBalanceRequest : IRequest<Result<TransferBalanceResponse>>
    {
        public long CustomerIdOut { get; set; }
        public long CustomerIdIn { get; set; }
        public decimal Value {  get; set; }
    }
}
