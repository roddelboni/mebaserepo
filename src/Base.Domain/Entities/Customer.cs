using Base.Domain.Primitives;

namespace Base.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer() { }

        public Customer(string name, string document, DateOnly birth)
        {
            Name = name;
            Document = document;
            BirthDate = birth.ToDateTime(TimeOnly.MinValue).ToUniversalTime();
        }

        public string Name { get; private set; }
        public string Document { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Wallet CustomerWallet { get; private set; }
        public User User { get; set; }

        public void AddWallet(Wallet wallet)
        {
            this.CustomerWallet = wallet;
        }
    }
}
