using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Misc.BrainStation.Models
{
    public record DeveloperDemoModel : BaseNopEntityModel
    {
        public DeveloperDemoModel()
        {
            AvailableDesignationOptions = [];
            AvailableStatusOptions = [];
        }

        [NopResourceDisplayName("Admin.Misc.DeveloperDemo.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Misc.DeveloperDemo.Fields.IsMVP")]
        public bool IsMVP { get; set; }

        [NopResourceDisplayName("Admin.Misc.DeveloperDemo.Fields.IsCertified")]
        public bool IsCertified { get; set; }

        [NopResourceDisplayName("Admin.Misc.DeveloperDemo.Fields.PictureThumbnailUrl")]
        public string PictureThumbnailUrl { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Admin.Misc.DeveloperDemo.Fields.Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Admin.Misc.DeveloperDemo.Fields.Status")]
        public int StatusId { get; set; }

        [NopResourceDisplayName("Admin.Misc.DeveloperDemo.Fields.Status")]
        public string StatusStr { get; set; }

        [NopResourceDisplayName("Admin.Misc.DeveloperDemo.Fields.Designation")]
        public int DesignationId { get; set; }

        [NopResourceDisplayName("Admin.Misc.DeveloperDemo.Fields.Designation")]
        public string DesignationStr { get; set; }

        public IList<SelectListItem> AvailableStatusOptions { get; set; }
        public IList<SelectListItem> AvailableDesignationOptions { get; set; }
    }
}
