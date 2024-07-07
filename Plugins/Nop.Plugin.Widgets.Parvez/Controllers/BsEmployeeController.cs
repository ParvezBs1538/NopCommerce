using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.Parvez.Domain;
using Nop.Plugin.Widgets.Parvez.Factories;
using Nop.Plugin.Widgets.Parvez.Models;
using Nop.Plugin.Widgets.Parvez.Services;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.Parvez.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class BsEmployeeController : BasePluginController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeModelFactory _employeeModelFactory;

        public BsEmployeeController(IEmployeeService employeeService, IEmployeeModelFactory employeeModelFactory)
        {
            _employeeService = employeeService;
            _employeeModelFactory = employeeModelFactory;
        }

        public async Task<IActionResult> List()
        {
            var searchModel = await _employeeModelFactory.PrepareEmployeeSearchModelAsync(new EmployeeSearchModel());
            return View("~/Plugins/Widgets.Parvez/Views/Employee/List.cshtml", searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(EmployeeSearchModel searchModel)
        {
            var model = await _employeeModelFactory.PrepareEmployeeListModelAsync(searchModel);
            return Json(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _employeeModelFactory.PrepareEmployeeModelAsync(new EmployeeModel(), null);
            return View("~/Plugins/Widgets.Parvez/Views/Employee/Create.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Create(EmployeeModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var employee = new BsEmployee
                {
                    Name = model.Name,
                    DesignationId = model.DesignationId,
                    StatusId = model.StatusId,
                    IsMVP = model.IsMVP,
                    IsCertified = model.IsCertified,
                    PictureId = model.PictureId,
                };

                await _employeeService.InsertEmployeeAsync(employee);

                return continueEditing ? RedirectToAction("Edit", new { id = employee.Id }) : RedirectToAction("List");
            }

            model = await _employeeModelFactory.PrepareEmployeeModelAsync(model, null);

            return View("~/Plugins/Widgets.Plugin/Views/Employee/Create.cshtml", model);
            //return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return RedirectToAction("List");

            var model = await _employeeModelFactory.PrepareEmployeeModelAsync(null, employee);

            return View("~/Plugins/Widgets.Parvez/Views/Employee/Edit.cshtml", model);
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Edit(EmployeeModel model, bool continueEditing)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(model.Id);
            if (employee == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                employee.Name = model.Name;
                employee.DesignationId = model.DesignationId;
                employee.StatusId = model.StatusId;
                employee.IsMVP = model.IsMVP;
                employee.IsCertified = model.IsCertified;
                employee.PictureId = model.PictureId;

                await _employeeService.UpdateEmployeeAsync(employee);
                return continueEditing ? RedirectToAction("Edit", new { id = employee.Id }) : RedirectToAction("List");
            }

            model = await _employeeModelFactory.PrepareEmployeeModelAsync(model, employee);

            return View("~/Plugins/Widgets.Parvez/Views/Employee/Edit.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeModel model)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(model.Id);
            if (employee == null)
                return RedirectToAction("List");

            await _employeeService.DeleteEmployeeAsync(employee);

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
                    var employee = await _employeeService.GetEmployeeByIdAsync(id);
                    if (employee != null)
                    {
                        await _employeeService.DeleteEmployeeAsync(employee);
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
