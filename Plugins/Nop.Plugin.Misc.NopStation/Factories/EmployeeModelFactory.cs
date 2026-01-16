using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Models;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Models.Media;

namespace Nop.Plugin.Misc.NopStation.Factories;

public class EmployeeModelFactory : IEmployeeModelFactory
{
    #region Fields

    private readonly ILocalizationService _localizationService;
    private readonly IPictureService _pictureService;

    #endregion

    #region Ctor

    public EmployeeModelFactory(ILocalizationService localizationService,
        IPictureService pictureService)
    {
        _localizationService = localizationService;
        _pictureService = pictureService;
    }

    #endregion

    #region Methods

    public async Task<IList<EmployeeModel>> PrepareEmployeeListModelAsync(IList<Employee> employees)
    {
        var model = new List<EmployeeModel>();

        foreach (var employee in employees)
            model.Add(await PrepareEmployeeModelAsync(employee));

        return model;
    }

    public async Task<EmployeeModel> PrepareEmployeeModelAsync(Employee employee)
    {
        var picture = await _pictureService.GetPictureByIdAsync(employee.PictureId);

        var pictureModel = new PictureModel
        {
            Id = employee.PictureId,
            AlternateText = "Picture of " + employee.Name,
            Title = "Picture of " + employee.Name,
            ThumbImageUrl = (await _pictureService.GetPictureUrlAsync(picture, 200)).Url,
            FullSizeImageUrl = (await _pictureService.GetPictureUrlAsync(picture)).Url,
        };

        return new EmployeeModel()
        {
            Id = employee.Id,
            Name = employee.Name,
            IsMVP = employee.IsMVP,
            IsNopCommerceCertified = employee.IsNopCommerceCertified,
            Picture = pictureModel,
            EmployeeStatus = employee.EmployeeStatus,
            EmployeeStatusStr = await _localizationService.GetLocalizedEnumAsync(employee.EmployeeStatus),
            EmployeeDesignation = employee.EmployeeDesignation,
            EmployeeDesignationStr = await _localizationService.GetLocalizedEnumAsync(employee.EmployeeDesignation)
        };
    }

    #endregion
}
