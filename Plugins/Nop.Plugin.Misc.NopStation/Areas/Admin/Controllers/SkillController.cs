using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.NopStation.Areas.Admin.Factories;
using Nop.Plugin.Misc.NopStation.Areas.Admin.Models;
using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Services;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.NopStation.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class SkillController : BasePluginController
    {
        private readonly ISkillService _skillService;
        private readonly ISkillModelFactory _skillModelFactory;

        public SkillController(ISkillService SkillService,
            ISkillModelFactory SkillModelFactory)
        {
            _skillService = SkillService;
            _skillModelFactory = SkillModelFactory;
        }

        public async Task<IActionResult> List()
        {
            var searchModel = await _skillModelFactory.PrepareSkillSearchModelAsync(new SkillSearchModel());

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Skill/List.cshtml", searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(SkillSearchModel searchModel)
        {
            var model = await _skillModelFactory.PrepareSkillListModelAsync(searchModel);

            return Json(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _skillModelFactory.PrepareSkillModelAsync(new SkillModel(), null);

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Skill/Create.cshtml", model);
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Create(SkillModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var skill = new Skill
                {
                    Name = model.Name
                };

                await _skillService.InsertSkillAsync(skill);

                return continueEditing ? RedirectToAction("Edit", new { id = skill.Id }) : RedirectToAction("List");
            }

            // If the model state is not valid, prepare the model again
            model = await _skillModelFactory.PrepareSkillModelAsync(model, null);

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Skill/Create.cshtml", model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Skill = await _skillService.GetSkillByIdAsync(id);
            if (Skill == null)
                return RedirectToAction("List");

            var model = await _skillModelFactory.PrepareSkillModelAsync(null, Skill);

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Skill/Edit.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Edit(SkillModel model, bool continueEditing)
        {
            var skill = await _skillService.GetSkillByIdAsync(model.Id);
            if (skill == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                skill.Name = model.Name;

                await _skillService.UpdateSkillAsync(skill);

                return continueEditing ? RedirectToAction("Edit", new { id = skill.Id }) : RedirectToAction("List");
            }

            model = await _skillModelFactory.PrepareSkillModelAsync(model, skill);

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Skill/Edit.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SkillModel model)
        {
            var skill = await _skillService.GetSkillByIdAsync(model.Id);
            if (skill == null)
                return RedirectToAction("List");

            await _skillService.DeleteSkillAsync(skill);

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
                    var skills = await _skillService.GetSkillByIdAsync(id);
                    if (skills != null)
                    {
                        await _skillService.DeleteSkillAsync(skills);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return Json(new { Result = true });
        }
    }
}
