using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Misc.ShopManagement.Models
{
    public record ProductModel : BaseNopEntityModel
    {
        public ProductModel()
        {
            AvailableDiscountOptions = [];
        }

        [NopResourceDisplayName("Admin.Misc.Product.Fields.PictureThumbnailUrl")]
        public string PictureThumbnailUrl { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Admin.Misc.Product.Fields.Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Admin.Misc.Product.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Misc.Product.Fields.Description")]
        public string Description { get; set; }  
        
        [NopResourceDisplayName("Admin.Misc.Product.Fields.Discount")]
        public int DiscountId { get; set; }

        [NopResourceDisplayName("Admin.Misc.Product.Fields.Discount")]
        public string DiscountStr { get; set; }

        public IList<SelectListItem> AvailableDiscountOptions { get; set; }
    }
}
