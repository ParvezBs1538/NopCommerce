using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.NopStation.Factories;
using Nop.Plugin.Misc.NopStation.Models;
using Nop.Plugin.Misc.NopStation.Services;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Services.Messages;

namespace Nop.Plugin.Misc.NopStation.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class DeveloperController : BasePluginController
    {
        private readonly IDeveloperService _DeveloperService;
        private readonly IDeveloperModelFactory _DeveloperModelFactory;
        protected readonly INotificationService _notificationService;

        public DeveloperController(IDeveloperService DeveloperService,
            IDeveloperModelFactory DeveloperModelFactory,
            INotificationService notificationService)
        {
            _DeveloperService = DeveloperService;
            _DeveloperModelFactory = DeveloperModelFactory;
            _notificationService = notificationService;
        }

        public async Task<IActionResult> List()
        {
            var searchModel = await _DeveloperModelFactory.PrepareDeveloperSearchModelAsync(new DeveloperSearchModel());

            return View("~/Plugins/Misc.NopStation/Views/Developer/List.cshtml", searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(DeveloperSearchModel searchModel)
        {
            var model = await _DeveloperModelFactory.PrepareDeveloperListModelAsync(searchModel);

            return Json(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _DeveloperModelFactory.PrepareDeveloperModelAsync(new DeveloperModel(), null);

            return View("~/Plugins/Misc.NopStation/Views/Developer/Create.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Create(DeveloperModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var Developer = new Developer
                {
                    DeveloperDesignationId = model.DeveloperDesignationId,
                    DeveloperStatusId = model.DeveloperStatusId,
                    IsMVP = model.IsMVP,
                    IsNopCommerceCertified = model.IsNopCommerceCertified,
                    Name = model.Name
                };

                await _DeveloperService.InsertDeveloperAsync(Developer);

                return continueEditing ? RedirectToAction("Edit", new { id = Developer.Id }) : RedirectToAction("List");
            }

            model = await _DeveloperModelFactory.PrepareDeveloperModelAsync(model, null);

            return View("~/Plugins/Misc.NopStation/Views/Developer/Create.cshtml", model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Developer = await _DeveloperService.GetDeveloperByIdAsync(id);
            if (Developer == null)
                return RedirectToAction("List");

            var model = await _DeveloperModelFactory.PrepareDeveloperModelAsync(null, Developer);

            return View("~/Plugins/Misc.NopStation/Views/Developer/Edit.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Edit(DeveloperModel model, bool continueEditing)
        {
            var Developer = await _DeveloperService.GetDeveloperByIdAsync(model.Id);
            if (Developer == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                Developer.DeveloperDesignationId = model.DeveloperDesignationId;
                Developer.DeveloperStatusId = model.DeveloperStatusId;
                Developer.IsMVP = model.IsMVP;
                Developer.IsNopCommerceCertified = model.IsNopCommerceCertified;
                Developer.Name = model.Name;

                await _DeveloperService.UpdateDeveloperAsync(Developer);

                return continueEditing ? RedirectToAction("Edit", new { id = Developer.Id }) : RedirectToAction("List");
            }

            model = await _DeveloperModelFactory.PrepareDeveloperModelAsync(model, Developer);

            return View("~/Plugins/Misc.NopStation/Views/Developer/Edit.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeveloperModel model)
        {
            var Developer = await _DeveloperService.GetDeveloperByIdAsync(model.Id);
            if (Developer == null)
                return RedirectToAction("List");

            await _DeveloperService.DeleteDeveloperAsync(Developer);

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSelected(ICollection<int> selectedIds)
        {
            if (selectedIds == null || selectedIds.Count == 0)
                return NoContent();

            try
            {
                foreach (int id in selectedIds)
                {
                    var developer = await _DeveloperService.GetDeveloperByIdAsync(id);
                    if (developer != null)
                    {
                        await _DeveloperService.DeleteDeveloperAsync(developer);
                    }
                }
                _notificationService.SuccessNotification("Admin.Catalog.Developers.Deleted");
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                // For now, we'll just rethrow the exception to be handled by a global exception handler if present
                throw;
            }

            return Json(new { Result = true });
        }
    }
}
