using FluentValidation;
using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Misc.NopStation.Validators
{
    public partial class DeveloperValidator : BaseNopValidator<DeveloperModel>
    {
        public DeveloperValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Admin.Misc.Developer.Fields.Name.Required"));
            RuleFor(x => x.DeveloperDesignationId).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Admin.Misc.Developer.Fields.Designation.Required"));

            SetDatabaseValidationRules<Developer>();
        }
    }
}
