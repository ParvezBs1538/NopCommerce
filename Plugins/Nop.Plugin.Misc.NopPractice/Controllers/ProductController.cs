using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.NopPractice.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class ProductController : BasePluginController
    {
        public async Task<IActionResult> List()
        {
            return View("~/Plugins/Misc.NopPractice/Views/Procuct/List.cshtml");
        }
    }
}
