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
}
