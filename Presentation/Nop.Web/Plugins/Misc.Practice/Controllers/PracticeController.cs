using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.WebApi.Frontend.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class PracticeController : BasePluginController
{
    public async Task<IActionResult> Configure()
    {
        //if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
        //    return AccessDeniedView();

        return View("~/Plugins/Misc.Practice/Views/Configure.cshtml");
    }
}
