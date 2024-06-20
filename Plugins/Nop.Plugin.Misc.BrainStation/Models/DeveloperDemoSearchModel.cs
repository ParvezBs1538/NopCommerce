using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.BrainStation.Models
{
    public record DeveloperDemoSearchModel : BaseSearchModel
    {
        public DeveloperDemoSearchModel()
        {
            AvailableStatusOptions = [];
            AvailableDesignationOptions = [];
        }

        [NopResourceDisplayName("Admin.Misc.DeveloperDemo.List.Name")]
        public string Name { get; set; }
        [NopResourceDisplayName("Admin.Misc.DeveloperDemo.List.Designation")]
        public int DesignationId { get; set; }
        [NopResourceDisplayName("Admin.Misc.DeveloperDemo.List.Status")]
        public int StatusId { get; set; }

        public IList<SelectListItem> AvailableStatusOptions { get; set; }
        public IList<SelectListItem> AvailableDesignationOptions { get; set; }
    }
}
