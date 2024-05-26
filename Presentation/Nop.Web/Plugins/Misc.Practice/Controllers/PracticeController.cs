using Microsoft.AspNetCore.Mvc;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.WebApi.Frontend.Controllers;

//[AutoValidateAntiforgeryToken]
[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class PracticeController : BasePluginController
{
    #region Fields

    //private readonly IPermissionService _permissionService;

    //#endregion

    //#region Ctor 

    //public PracticeController(IPermissionService permissionService)
    //{
    //    _permissionService = permissionService;
    //}

    #endregion

    #region Methods

    public async Task<IActionResult> Configure()
    {
        //if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
        //    return AccessDeniedView();

        return View("~/Plugins/Misc.Practice/Views/Configure.cshtml");
    }

    #endregion
}
