using Nop.Core;

namespace Nop.Plugin.Misc.BrainStation.Domain
{
    public class Teacher : BaseEntity
    {
        public string Name { get; set; }
        public int TeacherDesignationId { get; set; }
        public bool IsCertified { get; set; }
        public int PictureId { get; set; }

        public TeacherDesignation TeacherDesignation
        {
            get => (TeacherDesignation)TeacherDesignationId;
            set => TeacherDesignationId = (int)value;
        }
    }
}
