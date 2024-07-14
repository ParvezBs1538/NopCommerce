using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework;
using Nop.Plugin.Widgets.StudentSkill.Services;
using Nop.Plugin.Widgets.StudentSkill.Factories;
using Nop.Plugin.Widgets.StudentSkill.Models;
using Nop.Web.Framework.Controllers;
using Nop.Plugin.Widgets.StudentSkill.Domain;

namespace Nop.Plugin.Widgets.StudentSkill.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class StudentSkillController : BasePluginController
    {
        private readonly ISkillService _skillService;
        private readonly ISkillModelFactory _skillModelFactory;

        public StudentSkillController(ISkillService skillService, ISkillModelFactory skillModelFactory)
        {
            _skillService = skillService;
            _skillModelFactory = skillModelFactory;
        }

        public async Task<IActionResult> List()
        {
            var searchModel = await _skillModelFactory.PrepareSkillSearchModelAsync(new SkillSearchModel());

            return View("~/Plugins/Widgets.StudentSkill/Views/Skill/List.cshtml", searchModel);
            //return View(searchModel);
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

            return View("~/Plugins/Widgets.StudentSkill/Views/Skill/Create.cshtml", model);
            //return View(model);
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

            return View("~/Plugins/Widgets.StudentSkill/Views/Skill/Create.cshtml", model);
            //return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Skill = await _skillService.GetSkillByIdAsync(id);
            if (Skill == null)
                return RedirectToAction("List");

            var model = await _skillModelFactory.PrepareSkillModelAsync(null, Skill);

            return View("~/Plugins/Widgets.StudentSkill/Views/Skill/Edit.cshtml", model);
            //return View(model);
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

            return View("~/Plugins/Widgets.StudentSkill/Views/Skill/Edit.cshtml", model);
            //return View(model);
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
            catch (Exception)
            {
                throw;
            }

            return Json(new { Result = true });
        }
    }
}
