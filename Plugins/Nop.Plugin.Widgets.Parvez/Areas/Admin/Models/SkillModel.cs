using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.Parvez.Areas.Admin.Models
{
    public record SkillModel : BaseNopEntityModel
    {

        [NopResourceDisplayName("Admin.Widgets.Skill.Fields.Name")]
        public string Name { get; set; }
    }
}