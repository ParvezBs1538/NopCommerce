using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Factories;
using Nop.Plugin.Misc.NopStation.Services;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Misc.NopStation.Components;

public class DeveloperViewComponent : NopViewComponent
{
    #region Fields

    private readonly IDeveloperService _developerService;
    private readonly IDeveloperModelFactory _developerModelFactory;

    #endregion


    #region Ctor

    public DeveloperViewComponent(IDeveloperService developerService,
        IDeveloperModelFactory developerModelFactory)
    {
        _developerModelFactory = developerModelFactory;
        _developerService = developerService;
    }

    #endregion


    #region Methods

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        int categoryId = 0;

        if (widgetZone == PublicWidgetZones.CategoryDetailsTop)
        {
            var categoryModel = additionalData as CategoryModel;
            categoryId = categoryModel.Id;
        }

        var developers = await _developerService.SearchDevelopersAsync(
            name: null,
            statusId: (int)DeveloperStatus.Active,
            designationId: null
        );

        if (!developers.Any())
            return Content("");

        var model = await _developerModelFactory.PrepareDeveloperListModelAsync(developers, widgetZone, categoryId);

        return View("~/Plugins/Misc.NopStation/Views/Default.cshtml", model);
    }


    #endregion
}
