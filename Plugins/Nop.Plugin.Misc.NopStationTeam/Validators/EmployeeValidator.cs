using Nop.Plugin.Misc.NopStationTeam.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Nop.Plugin.Misc.NopStationTeam.Domain;

namespace Nop.Plugin.Misc.NopStationTeam.Validators
{
    public partial class EmployeeValidator : BaseNopValidator<EmployeeModel>
    {
        public EmployeeValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Admin.Misc.Employee.Fields.Name.Required"));
            RuleFor(x => x.Designation).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Admin.Misc.Employee.Fields.Designation.Required"));

            SetDatabaseValidationRules<Employee>();
        }
    }
}
