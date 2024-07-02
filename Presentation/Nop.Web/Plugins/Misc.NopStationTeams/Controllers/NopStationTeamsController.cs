using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.NopStationTeams.Models;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
namespace Nop.Plugin.Misc.NopStationTeams.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class NopStationTeamsController : BasePluginController
    {
        public IActionResult Index()
        {
            List<Profile> model = new List<Profile>
            {
                new Profile { Id = 1, Name = "Masud", Designation = "Developer", IsNopMVP = true, IsCertified = false },
                new Profile { Id = 2, Name = "Parvez", Designation = "Developer", IsNopMVP = false, IsCertified = true },
            };
            return View("~/Plugins/Misc.NopStationTeams/Views/Index.cshtml", model);
        }
        public async Task<IActionResult> Configure()
        {
            return View("~/Plugins/Misc.NopStationTeams/Views/Configure.cshtml");
        }
    }
}
