using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.StudentSkill.Models
{
    public record StudentSearchModel : BaseSearchModel
    {
        public StudentSearchModel()
        {
            AvailableStudentStatusOptions = [];
            AvailableStudentSkillOptions = [];
        }

        [NopResourceDisplayName("Admin.Widgets.Student.List.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Widgets.Student.List.Status")]
        public int StatusId { get; set; }

        [NopResourceDisplayName("Admin.Widgets.Student.List.Skill")]
        public int SkillId { get; set; }


        public IList<SelectListItem> AvailableStudentStatusOptions { get; set; }
        public IList<SelectListItem> AvailableStudentSkillOptions { get; set; }
    }
}
