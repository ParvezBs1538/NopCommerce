using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.NopStationTeams.Models;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.NopStationTeams.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class NopStationTeamsController : BasePluginController
{
    public async Task<IActionResult> Configure()
    {
        var model = new List<Parvez>
        {
            new Parvez {Id = 1, Name ="Masud"},
            new Parvez{Id = 2, Name = "Parvez"},
            new Parvez{Id = 1538, Name = "Masud Parvez"},
        };
        return View("~/Plugins/Misc.NopStationTeams/Views/Configure.cshtml", model);
    }
}
