using Base.Domain.Primitives;

namespace Base.Domain.Entities
{
    public class Blockchain : EntityConstruct
    {
        public Blockchain() { }
        public Blockchain(decimal value, DateTime transferData, long customerId, long? customerIdIn)
        {
            Value = value;
            TransferData = transferData;
            CustomerIdOut = customerId;
            CustomerIdIn = customerIdIn;
        }

        public decimal Value { get; private set; }
        public DateTime TransferData { get; private set; }
        public long CustomerIdOut { get; private set; }
        public long? CustomerIdIn { get; private set; }

    }
}
