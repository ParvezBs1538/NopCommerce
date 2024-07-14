using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.StudentSkill.Models
{
    public record StudentModel : BaseNopEntityModel
    {
        public StudentModel()
        {
            AvailableStudentStatusOptions = [];
            AvailableStudentSkillOptions = [];
        }

        [NopResourceDisplayName("Admin.Widgets.Student.Fields.PictureThumbnailUrl")]
        public string PictureThumbnailUrl { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Admin.Widgets.Student.Fields.Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Admin.Widgets.Student.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Widgets.Student.Fields.Age")]
        public int Age { get; set; }

        [NopResourceDisplayName("Admin.Widgets.Student.Fields.Skill")]
        public int SkillId { get; set; }

        [NopResourceDisplayName("Admin.Widgets.Student.Fields.Skill")]
        public string SkillName { get; set; }

        [NopResourceDisplayName("Admin.Widgets.Student.Fields.Status")]
        public int StatusId { get; set; }

        [NopResourceDisplayName("Admin.Widgets.Student.Fields.Status")]
        public string StatusName { get; set; }

        public IList<SelectListItem> AvailableStudentStatusOptions { get; set; }
        public IList<SelectListItem> AvailableStudentSkillOptions { get; set; }
    }
}
