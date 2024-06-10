using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Misc.BrainStation.Models
{
    public record TeacherModel : BaseNopEntityModel
    {
        public TeacherModel()
        {
            AvailableTeacherDesignationOptions = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.Misc.Teacher.Fields.Name")]
        public string Name { get; set; }


        [NopResourceDisplayName("Admin.Misc.Teacher.Fields.TeacherDesignation")]
        public int TeacherDesignationId { get; set; }

        [NopResourceDisplayName("Admin.Misc.Teacher.Fields.TeacherDesignation")]
        public string TeacherDesignationStr { get; set; }


        [NopResourceDisplayName("Admin.Misc.Teacher.Fields.IsCertified")]
        public bool IsCertified { get; set; }


        [NopResourceDisplayName("Admin.Misc.Teacher.Fields.PictureThumbnailUrl")]
        public string PictureThumbnailUrl { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Admin.Misc.Teacher.Fields.Picture")]
        public int PictureId { get; set; }

        public IList<SelectListItem> AvailableTeacherDesignationOptions { get; set; }
    }
}
