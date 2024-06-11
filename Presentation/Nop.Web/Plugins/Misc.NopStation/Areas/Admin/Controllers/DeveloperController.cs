using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.NopStation.Services;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Areas.Admin.Factories;
using Nop.Plugin.Misc.NopStation.Areas.Admin.Models;

namespace Nop.Plugin.Misc.NopStation.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class DeveloperController : BasePluginController
    {
        private readonly IDeveloperService _developerService;
        private readonly IDeveloperModelFactory _developerModelFactory;
        private readonly ISkillService _skillService;

        public DeveloperController(IDeveloperService developerService,
            IDeveloperModelFactory developerModelFactory,
            ISkillService skillService)
        {
            _developerService = developerService;
            _developerModelFactory = developerModelFactory;
            _skillService = skillService;
        }

        #region Utils

        protected virtual async Task SaveSkillMappingsAsync(Developer developer, DeveloperModel model)
        {
            var existingDeveloperSkils = await _skillService.GetDeveloperSkillMappingsByDeveloperIdAsync(developer.Id);

            //delete skills
            foreach (var existingDeveloperSkil in existingDeveloperSkils)
                if (!model.SelectedSkills.Contains(existingDeveloperSkil.SkillId))
                    await _skillService.DeleteDeveloperSkillMappingAsync(existingDeveloperSkil);

            var validSkills = await _skillService.GetSkillByIdsAsync(model.SelectedSkills.ToArray());
            //add skill
            foreach (var skillId in model.SelectedSkills)
            {
                if (validSkills.FirstOrDefault(s => s.Id == skillId) is null)
                    continue;

                if (await _skillService.FindDeveloperSkillMappingAsync(model.Id, skillId) == null)
                {
                    await _skillService.InsertDeveloperSkillMappingAsync(new DeveloperSkillMapping
                    {
                        DeveloperId = developer.Id,
                        SkillId = skillId
                    });
                }
            }
        }

        #endregion

        #region Methods

        public async Task<IActionResult> List()
        {
            var searchModel = await _developerModelFactory.PrepareDeveloperSearchModelAsync(new DeveloperSearchModel());

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Developer/List.cshtml", searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(DeveloperSearchModel searchModel)
        {
            var model = await _developerModelFactory.PrepareDeveloperListModelAsync(searchModel);

            return Json(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _developerModelFactory.PrepareDeveloperModelAsync(new DeveloperModel(), null);

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Developer/Create.cshtml", model);
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Create(DeveloperModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var developer = new Developer
                {
                    DeveloperDesignationId = model.DeveloperDesignationId,
                    DeveloperStatusId = model.DeveloperStatusId,
                    IsMVP = model.IsMVP,
                    IsNopCommerceCertified = model.IsNopCommerceCertified,
                    Name = model.Name,
                    PictureId = model.PictureId,
                };

                await _developerService.InsertDeveloperAsync(developer);
                await SaveSkillMappingsAsync(developer, model);

                return continueEditing ? RedirectToAction("Edit", new { id = developer.Id }) : RedirectToAction("List");
            }

            model = await _developerModelFactory.PrepareDeveloperModelAsync(model, null);

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Developer/Create.cshtml", model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var developer = await _developerService.GetDeveloperByIdAsync(id);
            if (developer == null)
                return RedirectToAction("List");

            var model = await _developerModelFactory.PrepareDeveloperModelAsync(null, developer);

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Developer/Edit.cshtml", model);
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Edit(DeveloperModel model, bool continueEditing)
        {
            var developer = await _developerService.GetDeveloperByIdAsync(model.Id);
            if (developer == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                developer.DeveloperDesignationId = model.DeveloperDesignationId;
                developer.DeveloperStatusId = model.DeveloperStatusId;
                developer.IsMVP = model.IsMVP;
                developer.IsNopCommerceCertified = model.IsNopCommerceCertified;
                developer.Name = model.Name;
                developer.PictureId = model.PictureId;

                await _developerService.UpdateDeveloperAsync(developer);
                await SaveSkillMappingsAsync(developer, model);

                return continueEditing ? RedirectToAction("Edit", new { id = developer.Id }) : RedirectToAction("List");
            }

            model = await _developerModelFactory.PrepareDeveloperModelAsync(model, developer);

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Developer/Edit.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeveloperModel model)
        {
            var developer = await _developerService.GetDeveloperByIdAsync(model.Id);
            if (developer == null)
                return RedirectToAction("List");

            await _developerService.DeleteDeveloperAsync(developer);

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
                    var developer = await _developerService.GetDeveloperByIdAsync(id);
                    if (developer != null)
                    {
                        await _developerService.DeleteDeveloperAsync(developer);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Json(new { Result = true });
        }

        #endregion
    }
}
