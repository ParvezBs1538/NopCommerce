using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.BrainStation.Factories;
using Nop.Plugin.Misc.BrainStation.Models;
using Nop.Plugin.Misc.BrainStation.Services;
using Nop.Plugin.Misc.BrainStation.Models;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.BrainStation.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class TeacherController : BasePluginController
    {
        private readonly ITeacherService _teacherService;
        private readonly ITeacherModelFactory _teacherModelFactory;

        public TeacherController(ITeacherService teacherService, ITeacherModelFactory teacherModelFactory)
        {
            _teacherModelFactory = teacherModelFactory;
            _teacherService = teacherService;
        }

        public async Task<IActionResult> List()
        {
            var searchModel = await _teacherModelFactory.PrepareTeacherSearchModelAsync(new TeacherSearchModel());
            return View("~/Plugins/Misc.BrainStation/Views/Teacher/List.cshtml", searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(TeacherSearchModel searchModel)
        {
            var model = await _teacherModelFactory.PrepareTeacherListModelAsync(searchModel);

            return Json(model);
        }
    }
}
