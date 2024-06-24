using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Misc.NopStation.Areas.Admin.Models
{
    public record DeveloperModel : BaseNopEntityModel
    {
        public DeveloperModel()
        {
            AvailableDeveloperStatusOptions = new List<SelectListItem>();
            AvailableDeveloperDesignationOptions = new List<SelectListItem>();
            AvailableDeveloperSkillOptions = new List<SelectListItem>();
            SelectedSkills = new List<int>();
            Skills = new List<string>();
        }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.Skills")]
        public IList<int> SelectedSkills { get; set; }
        [NopResourceDisplayName("Admin.Misc.Developer.Fields.Skills")]
        public List<string> Skills { get; set; }

        [NopResourceDisplayName("Admin.Misc.Developer.Fields.PictureThumbnailUrl")]
        public string PictureThumbnailUrl { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Admin.Misc.Developer.Fields.Picture")]
        public int PictureId { get; set; }



        [NopResourceDisplayName("Admin.Misc.Developer.Fields.Name")]
        public string Name { get; set; }

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
        public IList<SelectListItem> AvailableDeveloperSkillOptions { get; set; }
    }
}
