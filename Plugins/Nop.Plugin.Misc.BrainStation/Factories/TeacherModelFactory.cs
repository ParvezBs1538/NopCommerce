
using Nop.Plugin.Misc.BrainStation.Domain;
using Nop.Plugin.Misc.BrainStation.Models;
using Nop.Plugin.Misc.BrainStation.Services;
using Nop.Services;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.BrainStation.Factories
{
    public class TeacherModelFactory : ITeacherModelFactory
    {
        #region Fields

        private readonly ITeacherService _teacherService;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;

        #endregion

        #region Ctor

        public TeacherModelFactory(ITeacherService TeacherService,
            ILocalizationService localizationService,
            IPictureService pictureService)
        {
            _teacherService = TeacherService;
            _localizationService = localizationService;
            _pictureService = pictureService;
        }

        #endregion


        #region Methods

        public async Task<TeacherListModel> PrepareTeacherListModelAsync(TeacherSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            var teachers = await _teacherService.SearchTeachersAsync(searchModel.Name, searchModel.TeacherDesignationId,
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            //prepare list model
            var model = await new TeacherListModel().PrepareToGridAsync(searchModel, teachers, () =>
            {
                return teachers.SelectAwait(async teacher =>
                {
                    return await PrepareTeacherModelAsync(null, teacher, true);
                });
            });

            return model;
        }

        public async Task<TeacherModel> PrepareTeacherModelAsync(TeacherModel model, Teacher teacher, bool excludeProperties = false)
        {
            if (teacher != null)
            {
                if (model == null)
                {
                    //fill in model values from the entity
                    model = new TeacherModel()
                    {
                        Id = teacher.Id,
                        Name = teacher.Name,
                        TeacherDesignationId = teacher.TeacherDesignationId,
                        IsCertified = teacher.IsCertified,
                        PictureId = teacher.PictureId
                    };
                }
                model.TeacherDesignationStr = await _localizationService.GetLocalizedEnumAsync(teacher.TeacherDesignation);

                var picture = await _pictureService.GetPictureByIdAsync(teacher.PictureId);
                (model.PictureThumbnailUrl, _) = await _pictureService.GetPictureUrlAsync(picture, 75);
            }

            if (!excludeProperties)
            {
                model.AvailableTeacherDesignationOptions = [.. await TeacherDesignation.Lecturer.ToSelectListAsync()];
            }

            return model;
        }

        public async Task<TeacherSearchModel> PrepareTeacherSearchModelAsync(TeacherSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            searchModel.AvailableTeacherDesignationOptions = (await TeacherDesignation.Lecturer.ToSelectListAsync()).ToList();
            searchModel.AvailableTeacherDesignationOptions.Insert(0,
                new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }

        #endregion
    }
}
