using Nop.Core;

namespace Nop.Plugin.Misc.NopStation.Domain
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public int EmployeeDesignationId { get; set; }
        public bool IsMVP { get; set; }
        public bool IsNopCommerceCertified { get; set; }
        public int PictureId { get; set; }
        public int EmployeeStatusId { get; set; }

        public EmployeeStatus EmployeeStatus
        {
            get => (EmployeeStatus)EmployeeStatusId;
            set => EmployeeStatusId = (int)value;
        }

        public EmployeeDesignation EmployeeDesignation
        {
            get => (EmployeeDesignation)EmployeeDesignationId;
            set => EmployeeDesignationId = (int)value;
        }
    }
}
