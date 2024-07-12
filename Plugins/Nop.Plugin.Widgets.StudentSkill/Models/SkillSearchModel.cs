using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.StudentSkill.Models
{
    public record SkillSearchModel : BaseSearchModel
    {
        [NopResourceDisplayName("Admin.Widgets.Skill.List.Name")]
        public string Name { get; set; }
    }
}
