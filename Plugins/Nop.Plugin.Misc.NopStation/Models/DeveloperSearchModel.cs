using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.NopStation.Models
{
    public record DeveloperSearchModel : BaseSearchModel
    {
        public DeveloperSearchModel()
        {
            AvailableDeveloperStatusOptions = new List<SelectListItem>();
            //AvailableDeveloperDesignationOptions = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.Misc.Developer.List.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Misc.Developer.List.DeveloperStatus")]
        public int DeveloperStatusId { get; set; }

        public IList<SelectListItem> AvailableDeveloperStatusOptions { get; set; }
        //public IList<SelectListItem> AvailableDeveloperDesignationOptions { get; set; }
    }
}
