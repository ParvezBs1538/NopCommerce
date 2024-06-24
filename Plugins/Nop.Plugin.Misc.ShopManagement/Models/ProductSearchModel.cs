using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.ShopManagement.Models
{
    public record ProductSearchModel : BaseSearchModel
    {
        public ProductSearchModel()
        {
            AvailableDiscountOptions = [];
        }

        [NopResourceDisplayName("Admin.Misc.Product.List.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Misc.Product.List.Discount")]
        public int DiscountId { get; set; }

        public IList<SelectListItem> AvailableDiscountOptions { get; set; }
    }
}
