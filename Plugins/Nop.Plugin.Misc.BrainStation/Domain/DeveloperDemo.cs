using Nop.Core;

namespace Nop.Plugin.Misc.BrainStation.Domain
{
    public class DeveloperDemo : BaseEntity
    {
        public string Name { get; set; }
        public bool IsMVP { get; set; }
        public bool IsCertified { get; set; }
        public int PictureId { get; set; }
        public int StatusId { get; set; }
        public int DesignationId { get; set; }

        public Status Status 
        {
            get => (Status)StatusId;
            set => StatusId = (int)value; 
        }
        public Designation Designation 
        {
            get => (Designation)DesignationId;
            set => DesignationId = (int)value;
        }
    }
}
