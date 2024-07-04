using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.Parvez.Models
{
    public record EmployeeModel : BaseNopEntityModel
    {
        public EmployeeModel()
        {
            AvailableStatusOptions = [];
            AvailableDesignationOptions = [];
        }

        [NopResourceDisplayName("Widgets.Employee.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Widgets.Employee.Fields.PictureThumbnailUrl")]
        public string PictureThumbnailUrl { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Widgets.Employee.Fields.Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Widgets.Employee.Fields.Status")]
        public int StatusId { get; set; }

        [NopResourceDisplayName("Widgets.Employee.Fields.Status")]
        public int StatusStr { get; set; }

        [NopResourceDisplayName("Widgets.Employee.Fields.Designation")]
        public int DesignationStr { get; set; }

        [NopResourceDisplayName("Widgets.Employee.Fields.MVP")]
        public bool IsMVP { get; set; }

        [NopResourceDisplayName("Widgets.Employee.Fields.Certified")]
        public bool IsCertified { get; set; }

        public IList<SelectListItem> AvailableStatusOptions { get; set; }
        public IList<SelectListItem> AvailableDesignationOptions { get; set; }
    }
}
