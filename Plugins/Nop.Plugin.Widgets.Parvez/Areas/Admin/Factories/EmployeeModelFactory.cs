﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Widgets.Parvez.Areas.Admin.Models;
using Nop.Plugin.Widgets.Parvez.Domain;
using Nop.Plugin.Widgets.Parvez.Services;
using Nop.Services;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Widgets.Parvez.Areas.Admin.Factories
{
    public class EmployeeModelFactory : IEmployeeModelFactory
    {
        private readonly IEmployeeService _employeeService;
        private readonly ISkillService _skillService;
        private readonly IEmployeeSkillMappingService _employeeSkillMappingService;
        private readonly IPictureService _pictureService;
        private readonly ILocalizationService _localizationService;

        public EmployeeModelFactory(IEmployeeService employeeService, IPictureService pictureService, 
            ILocalizationService localizationService, ISkillService skillService, 
            IEmployeeSkillMappingService employeeSkillMappingService)
        {
            _employeeService = employeeService;
            _pictureService = pictureService;
            _localizationService = localizationService;
            _skillService = skillService;
            _employeeSkillMappingService = employeeSkillMappingService;
        }

        public async Task<EmployeeListModel> PrepareEmployeeListModelAsync(EmployeeSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            var employees = await _employeeService.SearchEmployeesAsync(searchModel.Name, searchModel.StatusId,
                searchModel.DesignationId,
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
                    model = new EmployeeModel
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        PictureId = employee.PictureId,
                        StatusId = employee.StatusId,
                        DesignationId = employee.DesignationId,
                        IsMVP = employee.IsMVP,
                        IsCertified = employee.IsCertified
                    };
                }

                //model.StatusStr = employee.EmployeeStatus.ToString();
                model.StatusStr = await _localizationService.GetLocalizedEnumAsync(employee.EmployeeStatus);

                //model.DesignationStr = employee.EmployeeDesignation.ToString();
                model.DesignationStr = await _localizationService.GetLocalizedEnumAsync(employee.EmployeeDesignation);

                var picture = await _pictureService.GetPictureByIdAsync(employee.PictureId);
                (model.PictureThumbnailUrl, _) = await _pictureService.GetPictureUrlAsync(picture, 75);

                var employeeSkills = await _employeeSkillMappingService.GetEmployeeSkillMappingsByEmployeeIdAsync(employee.Id);
                model.SelectedSkills = employeeSkills.Select(ds => ds.SkillId).ToList();

                var displayEmployeeSkills = await _skillService.GetSkillByIdsAsync(model.SelectedSkills.ToArray());
                model.Skills = displayEmployeeSkills.Select(ds => ds.Name).ToList();
            }

            if (!excludeProperties)
            {
                model.AvailableStatusOptions = (await EmployeeStatus.Active.ToSelectListAsync()).ToList();
                model.AvailableDesignationOptions = (await EmployeeDesignation.HeadOfNopStation.ToSelectListAsync()).ToList();

                var allSkills = await _skillService.GetAllSkillsAsync();
                model.AvailableEmployeeSkillOptions = allSkills.Select(skill => new SelectListItem
                {
                    Value = skill.Id.ToString(),
                    Text = skill.Name
                }).ToList();
            }

            return model;
        }

        public async Task<EmployeeSearchModel> PrepareEmployeeSearchModelAsync(EmployeeSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            searchModel.AvailableStatusOptions = (await EmployeeStatus.Active.ToSelectListAsync()).ToList();
            searchModel.AvailableStatusOptions.Insert(0,
                new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });

            searchModel.AvailableDesignationOptions = (await EmployeeDesignation.HeadOfNopStation.ToSelectListAsync()).ToList();
            searchModel.AvailableDesignationOptions.Insert(0,
                new SelectListItem
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
