using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.NopStation.Factories;
using Nop.Plugin.Misc.NopStation.Models;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Misc.NopStation.Components;

public class FooterBeforeViewComponent : NopViewComponent
{
    private readonly IModifyOrderModelFactory _modifyOrderModelFactory;

    public FooterBeforeViewComponent(IModifyOrderModelFactory modifyOrderModelFactory)
    {
        _modifyOrderModelFactory = modifyOrderModelFactory;
    }
  
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new FooterBeforeModel
        {
            Orders = await _modifyOrderModelFactory.PrepareFooterBeforeModelAsync()
        };

        return View("~/Plugins/Misc.NopStation/Views/Shared/Components/FooterBefore/Default.cshtml", model);
        // return View(model);
    }
}
