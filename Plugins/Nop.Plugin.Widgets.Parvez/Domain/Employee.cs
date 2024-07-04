using Nop.Core;

namespace Nop.Plugin.Widgets.Parvez.Domain;
public class Employee : BaseEntity
{
    public string Name { get; set; }
    public int PictureId { get; set; }
    public int StatusId { get; set; }
    public int DesignationId { get; set; }
    public bool IsMVP { get; set; }
    public bool IsCertified { get; set; }

    public EmployeeStatus EmployeeStatus
    {
        get => (EmployeeStatus)StatusId;
        set => DesignationId = (int)value;
    }

    public EmployeeDesignation EmployeeDesignation
    {
        get => (EmployeeDesignation)DesignationId;
        set => DesignationId = (int)value;
    }
}
