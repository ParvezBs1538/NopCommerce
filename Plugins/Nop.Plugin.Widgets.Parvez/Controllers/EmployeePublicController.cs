using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.Parvez.Factories;
using Nop.Plugin.Widgets.Parvez.Models;
using Nop.Plugin.Widgets.Parvez.Services;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.Parvez.Controllers
{
    public class EmployeePublicController : BasePluginController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeModelFactory _employeeModelFactory;

        public EmployeePublicController(IEmployeeService employeeService,
            IEmployeeModelFactory EmployeeModelFactory)
        {
            _employeeService = employeeService;
            _employeeModelFactory = EmployeeModelFactory;
        }

        /*public async Task<IActionResult> Index()
        {
            *//*var employee = await _employeeService.SearchEmployeesAsync(
                name: null,
                statusId: null,
                designationId: null,
                isCert: false,
                isMvp: false
            );*//*

            var employee = await _employeeService.GetAllEmployeesAsync();
            var model = await _employeeModelFactory.PrepareEmployeePublicModelAsync(employee);

            return View("~/Plugins/Widgets.Parvez/Views/Index.cshtml", model);
        }*/

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            var model = await _employeeModelFactory.PrepareEmployeePublicListModelAsync(employees);

            return View("~/Plugins/Widgets.Parvez/Views/Index.cshtml", model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee != null)
            {
                await _employeeService.DeleteEmployeeAsync(employee);
            }
            return RedirectToAction("Index");
        }
    }
}
