using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Base.Domain.Primitives
{
    [ExcludeFromCodeCoverage]
    public abstract class Entity
    {
        [JsonIgnore]
        public long Id { get; private set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; private set; }

        [JsonIgnore]
        public DateTime UpdatedAt { get; protected set; }

        [JsonIgnore]
        public DateTime? DeletedAt { get; private set; }

        [JsonIgnore]
        public bool IsDeleted { get; private set; }


        public Entity()
        {
            CreatedAt = UpdatedAt = DateTime.UtcNow;
            //_errors = new List<ErrorMessage>();
        }

        public void SetAsDeleted()
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }
}
