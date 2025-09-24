using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.NopStation.Models;

public record ModifyOrderModel : BaseNopEntityModel
{
    public int OrderId { get; set; }

    public string CustomerName { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }
}
