using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.NopStation.Areas.Admin.Models
{
    public record EmployeeSearchModel : BaseSearchModel
    {
        public EmployeeSearchModel()
        {
            AvailableEmployeeStatusOptions = new List<SelectListItem>();
            AvailableEmployeeDesignationOptions = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.Misc.Employee.List.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Misc.Employee.List.EmployeeStatus")]
        public int EmployeeStatusId { get; set; }

        [NopResourceDisplayName("Admin.Misc.Employee.List.EmployeeDesignation")]
        public int EmployeeDesignationId { get; set; }

        public IList<SelectListItem> AvailableEmployeeStatusOptions { get; set; }
        public IList<SelectListItem> AvailableEmployeeDesignationOptions { get; set; }
    }
}
