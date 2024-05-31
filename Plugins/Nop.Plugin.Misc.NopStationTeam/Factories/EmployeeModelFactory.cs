using Nop.Plugin.Misc.NopStationTeam.Domain;
using Nop.Plugin.Misc.NopStationTeam.Models;
using Nop.Plugin.Misc.NopStationTeam.Services;
using Nop.Services;
using Nop.Services.Localization;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.NopStationTeam.Factories
{
    public class EmployeeModelFactory : IEmployeeModelFactory
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILocalizationService _localizationService;

        public EmployeeModelFactory(IEmployeeService employeeService,
            ILocalizationService localizationService) 
        {
            _employeeService = employeeService;
            _localizationService = localizationService;
        }

        public async Task<EmployeeListModel> PrepareEmployeeListModelAsync(EmployeeSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            var employees = await _employeeService.SearchEmployeesAsync(searchModel.Name, searchModel.EmployeeStatusId,
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            //prepare list model
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
                    //fill in model values from the entity
                    model = new EmployeeModel()
                    {
                        Designation = employee.Designation,
                        EmployeeStatusId = employee.EmployeeStatusId,
                        Id = employee.Id,
                        IsMVP = employee.IsMVP,
                        IsNopCommerceCertified = employee.IsNopCommerceCertified,
                        Name = employee.Name
                    };
                }
                model.EmployeeStatusStr = await _localizationService.GetLocalizedEnumAsync(employee.EmployeeStatus);
            }

            if (!excludeProperties)
            {
                model.AvailableEmployeeStatusOptions = (await EmployeeStatus.Active.ToSelectListAsync()).ToList();
            }

            return model;
        }

        public async Task<EmployeeSearchModel> PrepareEmployeeSearchModelAsync(EmployeeSearchModel searchModel)
        { 
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            searchModel.AvailableEmployeeStatusOptions = (await EmployeeStatus.Active.ToSelectListAsync()).ToList();
            searchModel.AvailableEmployeeStatusOptions.Insert(0,
                new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
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
