using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Widgets.StudentSkill.Domain;
using Nop.Plugin.Widgets.StudentSkill.Models;
using Nop.Plugin.Widgets.StudentSkill.Services;
using Nop.Services;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Widgets.StudentSkill.Factories
{
    public class StudentModelFactory : IStudentModelFactory
    {
        private readonly IPictureService _pictureService;
        private readonly IStudentService _studentService;
        private readonly ILocalizationService _localizationService;
        private readonly ISkillService _skillService;

        public StudentModelFactory(IPictureService pictureService,
            IStudentService studentService,
            ILocalizationService localizationService,
            ISkillService skillService)
        {
            _pictureService = pictureService;
            _studentService = studentService;
            _localizationService = localizationService;
            _skillService = skillService;
        }

        public async Task<StudentListModel> PrepareStudentListModelAsync(StudentSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            var students = await _studentService.SearchStudentsAsync(searchModel.Name, 
                searchModel.StatusId,
                searchModel.SkillId,
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            var model = await new StudentListModel().PrepareToGridAsync(searchModel, students, () =>
            {
                return students.SelectAwait(async student =>
                {
                    return await PrepareStudentModelAsync(null, student, true);
                });
            });

            return model;
        }

        public async Task<StudentModel> PrepareStudentModelAsync(StudentModel model, Student student, bool excludeProperties = false)
        {
            if (student != null)
            {
                if (model == null)
                {
                    model = new StudentModel()
                    {
                        Id = student.Id,
                        Name = student.Name,
                        Age = student.Age,
                        StatusId = student.StatusId,
                        SkillId = student.SkillId,
                        PictureId = student.PictureId,
                    };
                }

                var studentSkill = await _skillService.GetSkillByIdAsync(student.SkillId);
                model.SkillName = studentSkill?.Name;

                model.StatusName = await _localizationService.GetLocalizedEnumAsync(student.StudentStatus);
                
                //var currentStudent = await _studentService.GetStudentByIdAsync(student.Id);
                //var studentSkill = await _skillService.GetSkillByIdAsync(currentStudent.SkillId);
                //model.SkillName = studentSkill.ToString();

                var picture = await _pictureService.GetPictureByIdAsync(student.PictureId);
                (model.PictureThumbnailUrl, _) = await _pictureService.GetPictureUrlAsync(picture, 75);
            }

            if (!excludeProperties)
            {
                model.AvailableStudentStatusOptions = [.. await StudentStatus.Active.ToSelectListAsync()];

                var allSkills = await _skillService.GetAllSkillsAsync();
                model.AvailableStudentSkillOptions = allSkills.Select(skill => new SelectListItem
                {
                    Value = skill.Id.ToString(),
                    Text = skill.Name
                }).ToList();
            }

            return model;
        }

        public async Task<StudentSearchModel> PrepareStudentSearchModelAsync(StudentSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            searchModel.AvailableStudentStatusOptions = (await StudentStatus.Active.ToSelectListAsync()).ToList();
            searchModel.AvailableStudentStatusOptions.Insert(0,
                new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });

            // Prepare available student skill options
            var allSkills = await _skillService.GetAllSkillsAsync();
            searchModel.AvailableStudentSkillOptions = allSkills.Select(skill => new SelectListItem
            {
                Value = skill.Id.ToString(),
                Text = skill.Name
            }).ToList();
            searchModel.AvailableStudentSkillOptions.Insert(0, new SelectListItem
            {
                Text = "All",
                Value = "0"
            });

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }
    }
}
