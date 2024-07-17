using Nop.Plugin.Widgets.Parvez.Domain;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.Parvez.Models
{
    public record EmployeePublicModel : BaseNopEntityModel
    {
        public string Name { get; set; }
        public bool IsMVP { get; set; }
        public bool IsCertified { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }
        public string EmployeeStatusStr { get; set; }
    }
}
