using Nop.Core;

namespace Nop.Plugin.Misc.NopStation.Domain
{
    public class DeveloperSkillMapping : BaseEntity
    {
        public int DeveloperId { get; set; }
        public int SkillId { get; set; }
    }
}
