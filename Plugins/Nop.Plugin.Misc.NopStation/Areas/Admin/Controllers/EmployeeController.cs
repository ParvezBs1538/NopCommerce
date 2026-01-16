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
    public class EmployeeController : BasePluginController
    {
        #region Fields

        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeModelFactory _employeeModelFactory;

        #endregion

        #region Ctor

        public EmployeeController(IEmployeeService employeeService,
            IEmployeeModelFactory employeeModelFactory)
        {
            _employeeService = employeeService;
            _employeeModelFactory = employeeModelFactory;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> List()
        {
            var searchModel = await _employeeModelFactory.PrepareEmployeeSearchModelAsync(new EmployeeSearchModel());

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Employee/List.cshtml", searchModel);
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

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Employee/Create.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Create(EmployeeModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    EmployeeDesignationId = model.EmployeeDesignationId,
                    EmployeeStatusId = model.EmployeeStatusId,
                    IsMVP = model.IsMVP,
                    IsNopCommerceCertified = model.IsNopCommerceCertified,
                    Name = model.Name,
                    PictureId = model.PictureId,
                };

                await _employeeService.InsertEmployeeAsync(employee);

                return continueEditing ? RedirectToAction("Edit", new { id = employee.Id }) : RedirectToAction("List");
            }

            model = await _employeeModelFactory.PrepareEmployeeModelAsync(model, null);

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Employee/Create.cshtml", model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return RedirectToAction("List");

            var model = await _employeeModelFactory.PrepareEmployeeModelAsync(null, employee);

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Employee/Edit.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Edit(EmployeeModel model, bool continueEditing)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(model.Id);
            if (employee == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                employee.EmployeeDesignationId = model.EmployeeDesignationId;
                employee.EmployeeStatusId = model.EmployeeStatusId;
                employee.IsMVP = model.IsMVP;
                employee.IsNopCommerceCertified = model.IsNopCommerceCertified;
                employee.Name = model.Name;
                employee.PictureId = model.PictureId;

                await _employeeService.UpdateEmployeeAsync(employee);

                return continueEditing ? RedirectToAction("Edit", new { id = employee.Id }) : RedirectToAction("List");
            }

            model = await _employeeModelFactory.PrepareEmployeeModelAsync(model, employee);

            return View("~/Plugins/Misc.NopStation/Areas/Admin/Views/Employee/Edit.cshtml", model);
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

        #endregion
    }
}
