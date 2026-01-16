using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Misc.NopStation.Areas.Admin.Models
{
    public record EmployeeModel : BaseNopEntityModel
    {
        public EmployeeModel()
        {
            AvailableEmployeeStatusOptions = new List<SelectListItem>();
            AvailableEmployeeDesignationOptions = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.Misc.Employee.Fields.PictureThumbnailUrl")]
        public string PictureThumbnailUrl { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Admin.Misc.Employee.Fields.Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Admin.Misc.Employee.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Misc.Employee.Fields.EmployeeDesignation")]
        public int EmployeeDesignationId { get; set; }

        [NopResourceDisplayName("Admin.Misc.Employee.Fields.EmployeeDesignation")]
        public string EmployeeDesignationStr { get; set; }

        [NopResourceDisplayName("Admin.Misc.Employee.Fields.IsMVP")]
        public bool IsMVP { get; set; }

        [NopResourceDisplayName("Admin.Misc.Employee.Fields.IsNopCommerceCertified")]
        public bool IsNopCommerceCertified { get; set; }

        [NopResourceDisplayName("Admin.Misc.Employee.Fields.EmployeeStatus")]
        public int EmployeeStatusId { get; set; }

        [NopResourceDisplayName("Admin.Misc.Employee.Fields.EmployeeStatus")]
        public string EmployeeStatusStr { get; set; }

        public IList<SelectListItem> AvailableEmployeeStatusOptions { get; set; }
        public IList<SelectListItem> AvailableEmployeeDesignationOptions { get; set; }
    }
}
