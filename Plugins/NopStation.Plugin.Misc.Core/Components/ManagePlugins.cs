using Microsoft.AspNetCore.Mvc;

namespace NopStation.Plugin.Misc.Core.Components;

public class ManagePluginsViewComponent : NopStationViewComponent
{
    public IViewComponentResult Invoke(string widgetZone, object additionalData)
    {
        return View();
    }
}
