using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.NopStation.Models;

public record FooterBeforeModel : BaseNopModel
{
    public FooterBeforeModel()
    {
        Orders = new List<ModifyOrderModel>();
    }

    public IList<ModifyOrderModel> Orders { get; set; }
}
