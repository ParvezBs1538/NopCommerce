using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Web.Framework.Models;
using Nop.Web.Models.Media;

namespace Nop.Plugin.Misc.NopStation.Models;

public record EmployeeModel : BaseNopEntityModel
{
    public EmployeeModel()
    {
        Picture = new PictureModel();
    }

    public string Name { get; set; }

    public PictureModel Picture { get; set; }

    public bool IsMVP { get; set; }

    public bool IsNopCommerceCertified { get; set; }

    public EmployeeStatus EmployeeStatus { get; set; }

    public string EmployeeStatusStr { get; set; }

    public EmployeeDesignation EmployeeDesignation { get; set; }

    public string EmployeeDesignationStr { get; set; }
}
