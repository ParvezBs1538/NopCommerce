using Nop.Core;

namespace Nop.Plugin.Misc.NopStation.Domain;

public class ModifyOrder : BaseEntity
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime UpdatedOnUtc { get; set; }
}
