using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.NopStation.Models
{
    public record SkillSearchModel : BaseSearchModel
    {
        [NopResourceDisplayName("Admin.Misc.Skill.List.Name")]
        public string Name { get; set; }
    }
}
