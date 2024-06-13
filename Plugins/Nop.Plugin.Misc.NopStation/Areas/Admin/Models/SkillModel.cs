using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.NopStation.Areas.Admin.Models
{
    public record SkillModel : BaseNopEntityModel
    {

        [NopResourceDisplayName("Admin.Misc.Skill.Fields.Name")]
        public string Name { get; set; }
    }
}
