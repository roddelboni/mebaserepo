using Base.Domain.Primitives;

namespace Base.Domain.Entities
{
    public class Wallet : Entity
    {
        public Wallet() { }
        public Wallet(Guid number)
        {
            WalletNumber = number;
        }

        public Wallet(Guid number, decimal balance)
        {
            WalletNumber = number;
            Balance = balance;
        }

        public Guid WalletNumber { get; private set; }
        public decimal Balance { get; private set; }
        public long CustomerId { get; private set; }
        public Customer Customer { get; private set; }

        public void Update(Wallet wallet)
        {
            WalletNumber = wallet.WalletNumber;
            Balance = wallet.Balance;
            CustomerId = wallet.CustomerId;           
        }
    }
}
