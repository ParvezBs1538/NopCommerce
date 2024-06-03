using Nop.Core;

namespace Nop.Plugin.Misc.NopStation.Domain
{
    public class Developer : BaseEntity
    {
        public string Name { get; set; }
        //public string Designation { get; set; }
        public int DeveloperDesignationId { get; set;}
        public bool IsMVP { get; set; }
        public bool IsNopCommerceCertified { get; set; }
        public int DeveloperStatusId { get; set; }

        public DeveloperStatus DeveloperStatus
        {
            get => (DeveloperStatus)DeveloperStatusId;
            set => DeveloperStatusId = (int)value;
        }
        public DeveloperDesignation DeveloperDesignation
        {
            get => (DeveloperDesignation)DeveloperDesignationId;
            set => DeveloperDesignationId = (int)value;
        }
    }
    public enum DeveloperStatus
    {
        Active = 10,

        Inactive = 20,

        Blocked = 30
    }
    public enum DeveloperDesignation
    {
        Head_Of_nopStation = 10,

        Principal_Engineer = 20,

        Project_Manager = 30,

        Lead_Engineer = 40,

        Senior_Software_Engineer = 50,

        Software_Engineer = 60,

        Associate_Software_Engineer = 70,

        Trainee = 80
    }
}
