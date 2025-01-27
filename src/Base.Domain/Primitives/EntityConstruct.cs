using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Base.Domain.Primitives
{
    [ExcludeFromCodeCoverage]
    public class EntityConstruct
    {
        [JsonIgnore]
        public long Id { get; private set; }
    }
}
