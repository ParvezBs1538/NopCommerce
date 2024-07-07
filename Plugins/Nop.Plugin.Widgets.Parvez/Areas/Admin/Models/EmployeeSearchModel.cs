using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.Parvez.Areas.Admin.Models
{
    public record EmployeeSearchModel : BaseSearchModel
    {
        public EmployeeSearchModel()
        {
            AvailableStatusOptions = [];
            AvailableDesignationOptions = [];
        }

        [NopResourceDisplayName("Admin.Widgets.Employee.List.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Widgets.Employee.List.Status")]
        public int StatusId { get; set; }

        [NopResourceDisplayName("Admin.Widgets.Employee.List.Designation")]
        public int DesignationId { get; set; }

        public IList<SelectListItem> AvailableStatusOptions { set; get; }
        public IList<SelectListItem> AvailableDesignationOptions { set; get; }
    }
}
