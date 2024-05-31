using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.NopStationTeam.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class NopstationTeamController : BasePluginController
    {
        public async Task<IActionResult> Configure()
        {
            return View("~/Plugins/Misc.NopStationTeam/Views/Configure.cshtml");
        }
    }
}
