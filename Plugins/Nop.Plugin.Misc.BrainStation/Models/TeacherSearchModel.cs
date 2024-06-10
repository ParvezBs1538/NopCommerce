using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.BrainStation.Models
{
    public record TeacherSearchModel : BaseSearchModel
    {
        public TeacherSearchModel() 
        {
            AvailableTeacherDesignationOptions = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.Misc.Teacher.Fields.Name")]
        public string Name { get; set; }


        [NopResourceDisplayName("Admin.Misc.Teacher.Fields.TeacherDesignation")]
        public int TeacherDesignationId { get; set; }

        public IList<SelectListItem> AvailableTeacherDesignationOptions { get; set; }
    }
}
