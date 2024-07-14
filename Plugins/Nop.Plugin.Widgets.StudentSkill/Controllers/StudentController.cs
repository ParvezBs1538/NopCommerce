using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Plugin.Widgets.StudentSkill.Services;
using Nop.Plugin.Widgets.StudentSkill.Factories;
using Nop.Plugin.Widgets.StudentSkill.Models;
using Nop.Plugin.Widgets.StudentSkill.Domain;

namespace Nop.Plugin.Widgets.StudentSkill.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class StudentController : BasePluginController
    {
        private readonly IStudentService _studentService;
        private readonly ISkillService _skillService;
        private readonly IStudentModelFactory _studentModelFactory;

        public StudentController(IStudentService studentService, 
            ISkillService skillService, IStudentModelFactory studentModelFactory)
        {
            _studentService = studentService;
            _skillService = skillService;
            _studentModelFactory = studentModelFactory;
        }

        public async Task<IActionResult> List()
        {
            var searchModel = await _studentModelFactory.PrepareStudentSearchModelAsync(new StudentSearchModel());

            //return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Student/List.cshtml", searchModel);
            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(StudentSearchModel searchModel)
        {
            var model = await _studentModelFactory.PrepareStudentListModelAsync(searchModel);

            return Json(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _studentModelFactory.PrepareStudentModelAsync(new StudentModel(), null);

            //return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Student/Create.cshtml", model);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Create(StudentModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Name = model.Name,
                    Age = model.Age,
                    PictureId = model.PictureId,
                    StatusId = model.StatusId,
                    SkillId = model.SkillId,
                };

                await _studentService.InsertStudentAsync(student);

                return continueEditing ? RedirectToAction("Edit", new { id = student.Id }) : RedirectToAction("List");
            }

            model = await _studentModelFactory.PrepareStudentModelAsync(model, null);

            //return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Developer/Create.cshtml", model);
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return RedirectToAction("List");

            var model = await _studentModelFactory.PrepareStudentModelAsync(null, student);

            //return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Developer/Edit.cshtml", model);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Edit(StudentModel model, bool continueEditing)
        {
            var student = await _studentService.GetStudentByIdAsync(model.Id);
            if (student == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                student.Name = model.Name;
                student.Age = model.Age;
                student.SkillId = model.SkillId;
                student.StatusId = model.StatusId;
                student.PictureId = model.PictureId;

                await _studentService.UpdateStudentAsync(student);
                return continueEditing ? RedirectToAction("Edit", new { id = student.Id }) : RedirectToAction("List");
            }

            model = await _studentModelFactory.PrepareStudentModelAsync(model, student);

            //return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Developer/Edit.cshtml", model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(StudentModel model)
        {
            var student = await _studentService.GetStudentByIdAsync(model.Id);
            if (student == null)
                return RedirectToAction("List");

            await _studentService.DeleteStudentAsync(student);

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
                    var student = await _studentService.GetStudentByIdAsync(id);
                    if (student != null)
                    {
                        await _studentService.DeleteStudentAsync(student);
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
