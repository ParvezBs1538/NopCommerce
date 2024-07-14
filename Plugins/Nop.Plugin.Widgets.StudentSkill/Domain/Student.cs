using Nop.Core;

namespace Nop.Plugin.Widgets.StudentSkill.Domain
{
    public class Student : BaseEntity
    {
        public int PictureId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int StatusId { get; set; }
        public int SkillId { get; set; }
        public StudentStatus StudentStatus
        {
            get => (StudentStatus)StatusId;
            set => StatusId = (int)value;
        }
    }
}
