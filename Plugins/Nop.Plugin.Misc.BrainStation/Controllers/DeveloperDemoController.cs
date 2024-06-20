using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Plugin.Misc.BrainStation.Services;
using Nop.Plugin.Misc.BrainStation.Factories;
using Nop.Plugin.Misc.BrainStation.Models;
using Nop.Plugin.Misc.BrainStation.Domain;

namespace Nop.Plugin.Misc.BrainStation.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class DeveloperDemoController : BasePluginController
    {
        private readonly IDeveloperDemoService _developerDemoService;
        private readonly IDeveloperDemoModelFactory _developerDemoModelFactory;

        public DeveloperDemoController(IDeveloperDemoService developerDemoService,
            IDeveloperDemoModelFactory developerDemoModelFactory) 
        {
            _developerDemoService = developerDemoService;
            _developerDemoModelFactory = developerDemoModelFactory;
        }

        public async Task<IActionResult> List()
        {
            var searchModel = await _developerDemoModelFactory.PrepareDeveloperDemoSearchModelAsync(new DeveloperDemoSearchModel());

            return View("~/Plugins/Misc.BrainStation/Views/DeveloperDemo/List.cshtml", searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(DeveloperDemoSearchModel searchModel)
        {
            var model = await _developerDemoModelFactory.PrepareDeveloperDemoListModelAsync(searchModel);

            return Json(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _developerDemoModelFactory.PrepareDeveloperDemoModelAsync(new DeveloperDemoModel(), null);

            return View("~/Plugins/Misc.BrainStation/Views/DeveloperDemo/Create.cshtml", model);
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Create(DeveloperDemoModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var developer = new DeveloperDemo
                {
                    DesignationId = model.DesignationId,
                    StatusId = model.StatusId,
                    IsMVP = model.IsMVP,
                    IsCertified = model.IsCertified,
                    Name = model.Name,
                    PictureId = model.PictureId,
                };
                await _developerDemoService.InsertDeveloperDemoAsync(developer);

                return continueEditing ? RedirectToAction("Edit", new { id = developer.Id }) : RedirectToAction("List");
            }

            model = await _developerDemoModelFactory.PrepareDeveloperDemoModelAsync(model, null);

            return View("~/Plugins/Misc.BrainStation/Views/DeveloperDemo/Create.cshtml", model);
        }
    }
}
