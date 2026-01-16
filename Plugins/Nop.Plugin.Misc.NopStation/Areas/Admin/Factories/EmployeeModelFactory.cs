using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Misc.NopStation.Areas.Admin.Models;
using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Services;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.NopStation.Areas.Admin.Factories
{
    public class EmployeeModelFactory : IEmployeeModelFactory
    {
        #region Fields

        private readonly IEmployeeService _employeeService;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;

        #endregion

        #region Ctor

        public EmployeeModelFactory(IEmployeeService employeeService,
            ILocalizationService localizationService,
            IPictureService pictureService)
        {
            _employeeService = employeeService;
            _localizationService = localizationService;
            _pictureService = pictureService;
        }

        #endregion

        #region Methods

        public async Task<EmployeeListModel> PrepareEmployeeListModelAsync(EmployeeSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            var employees = await _employeeService.SearchEmployeesAsync(searchModel.Name, searchModel.EmployeeStatusId,
                searchModel.EmployeeDesignationId,
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Admin.Misc.Employees"] = "Employees",
                ["Admin.Misc.Employees.AddNew"] = "Add new Employee",
                ["Admin.Misc.Employees.EditDetails"] = "Edit Employee details",
                ["Admin.Misc.Employees.BackToList"] = "back to Employee list",

                ["Admin.Misc.Employee.Fields.Picture"] = "Picture",
                ["Admin.Misc.Employee.Fields.Picture.Hint"] = "Enter Picture.",
                ["Admin.Misc.Employee.Fields.Name"] = "Name",
                ["Admin.Misc.Employee.Fields.EmployeeDesignation"] = "Designation",
                ["Admin.Misc.Employee.Fields.IsMVP"] = "Is MVP",
                ["Admin.Misc.Employee.Fields.IsNopCommerceCertified"] = "Is certified",
                ["Admin.Misc.Employee.Fields.EmployeeStatus"] = "Status",
                ["Admin.Misc.Employee.Fields.Name.Hint"] = "Enter Employee name.",
                ["Admin.Misc.Employee.Fields.EmployeeDesignation.Hint"] = "Enter Employee designation.",
                ["Admin.Misc.Employee.Fields.IsMVP.Hint"] = "Check if Employee is MVP.",
                ["Admin.Misc.Employee.Fields.IsNopCommerceCertified.Hint"] = "Check if Employee is certified.",
                ["Admin.Misc.Employee.Fields.EmployeeStatus.Hint"] = "Select Employee status.",

                ["Admin.Misc.Employee.List.Name"] = "Name",
                ["Admin.Misc.Employee.List.EmployeeStatus"] = "Status",
                ["Admin.Misc.Employee.List.Name.Hint"] = "Search by Employee name.",
                ["Admin.Misc.Employee.List.EmployeeStatus.Hint"] = "Search by Employee status.",
                ["Admin.Misc.Employee.List.EmployeeDesignation"] = "Designation",
                ["Admin.Misc.Employee.List.EmployeeDesignation.Hint"] = "Search by Designation status.",

                ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.HeadOfNopStation"] = "Head of nopStation",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.PrincipalEngineer"] = "Principal Engineer",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.ProjectManager"] = "Project Manager",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.LeadEngineer"] = "Lead Engineer",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.SeniorSoftwareEngineer"] = "Sr. Software Engineer",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.SoftwareEngineer"] = "Software Engineer",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.AssociateSoftwareEngineer"] = "Associate Software Engineer",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.Trainee"] = "Trainee"
            });

            var model = await new EmployeeListModel().PrepareToGridAsync(searchModel, employees, () =>
            {
                return employees.SelectAwait(async employee =>
                {
                    return await PrepareEmployeeModelAsync(null, employee, true);
                });
            });

            return model;
        }

        public async Task<EmployeeModel> PrepareEmployeeModelAsync(EmployeeModel model, Employee employee, bool excludeProperties = false)
        {
            if (employee != null)
            {
                if (model == null)
                {
                    model = new EmployeeModel()
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        EmployeeDesignationId = employee.EmployeeDesignationId,
                        EmployeeStatusId = employee.EmployeeStatusId,
                        IsMVP = employee.IsMVP,
                        IsNopCommerceCertified = employee.IsNopCommerceCertified,
                        PictureId = employee.PictureId,
                    };
                }

                model.EmployeeStatusStr = await _localizationService.GetLocalizedEnumAsync(employee.EmployeeStatus);

                model.EmployeeDesignationStr = await _localizationService.GetLocalizedEnumAsync(employee.EmployeeDesignation);

                var picture = await _pictureService.GetPictureByIdAsync(employee.PictureId);
                (model.PictureThumbnailUrl, _) = await _pictureService.GetPictureUrlAsync(picture, 75);
            }

            if (!excludeProperties)
            {
                model.AvailableEmployeeStatusOptions = [.. await EmployeeStatus.Active.ToSelectListAsync()];

                model.AvailableEmployeeDesignationOptions = [.. await EmployeeDesignation.Trainee.ToSelectListAsync()];
            }

            return model;
        }

        public async Task<EmployeeSearchModel> PrepareEmployeeSearchModelAsync(EmployeeSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            searchModel.AvailableEmployeeStatusOptions = (await EmployeeStatus.Active.ToSelectListAsync()).ToList();
            searchModel.AvailableEmployeeStatusOptions.Insert(0,
                new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });

            searchModel.AvailableEmployeeDesignationOptions = (await EmployeeDesignation.Trainee.ToSelectListAsync()).ToList();
            searchModel.AvailableEmployeeDesignationOptions.Insert(0,
                new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });

            searchModel.SetGridPageSize();

            return searchModel;
        }

        #endregion
    }
}
