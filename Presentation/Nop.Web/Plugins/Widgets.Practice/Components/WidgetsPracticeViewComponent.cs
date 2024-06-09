using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.Practice.Components
{
    public class WidgetsPracticeViewComponent : NopViewComponent
    {
        public IViewComponentResult Invoke(string widgetNone, object additionalData)
        {
            return View("~/Plugins/Widgets.Practice/Views/Default.cshtml");
        }
    }
}
