using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Misc.NopStation.Models
{
    public record DeveloperModel : BaseNopEntityModel
    {
        public DeveloperModel()
        {
            AvailableDeveloperStatusOptions = new List<SelectListItem>();
            AvailableDeveloperDesignationOptions = new List<SelectListItem>();
        }

        [UIHint("Picture")]
        [NopResourceDisplayName("Admin.Misc.Developer.Fields.Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.Name")]
        public string Name { get; set; }

        //[NopResourceDisplayName("Admin.Misc.Developer.Fields.Designation")]
        //public string Designation { get; set; }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.DeveloperDesignation")]
        public int DeveloperDesignationId { get; set; }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.DeveloperDesignation")]
        public string DeveloperDesignationStr { get; set; }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.IsMVP")]
        public bool IsMVP { get; set; }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.IsNopCommerceCertified")]
        public bool IsNopCommerceCertified { get; set; }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.DeveloperStatus")]
        public int DeveloperStatusId { get; set; }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.DeveloperStatus")]
        public string DeveloperStatusStr { get; set; }

        public IList<SelectListItem> AvailableDeveloperStatusOptions { get; set; }
        public IList<SelectListItem> AvailableDeveloperDesignationOptions { get; set; }
    }
}
