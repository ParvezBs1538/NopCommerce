using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.NopStation.Models
{
    public record DeveloperModel : BaseNopEntityModel
    {
        public DeveloperModel()
        {
            AvailableDeveloperStatusOptions = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.Designation")]
        public string Designation { get; set; }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.IsMVP")]
        public bool IsMVP { get; set; }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.IsNopCommerceCertified")]
        public bool IsNopCommerceCertified { get; set; }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.DeveloperStatus")]
        public int DeveloperStatusId { get; set; }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.DeveloperStatus")]
        public string DeveloperStatusStr { get; set; }

        public IList<SelectListItem> AvailableDeveloperStatusOptions { get; set; }
    }
}
