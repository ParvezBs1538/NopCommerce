using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.Parvez.Components
{
    public class EmployeeViewComponent : NopViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            return View("~/Plugins/Widgets.Parvez/Views/Default.cshtml");
        }
    }
}
