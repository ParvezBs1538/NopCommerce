using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.Parvez.Factories;
using Nop.Plugin.Widgets.Parvez.Models;
using Nop.Plugin.Widgets.Parvez.Services;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.Parvez.Controllers
{
    public class EmployeeController : BasePluginController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IBsEmployeeModelFactory _dbEmployeeModelFactory;

        public EmployeeController(IEmployeeService employeeService,
            IBsEmployeeModelFactory dbEmployeeModelFactory)
        {
            _employeeService = employeeService;
            _dbEmployeeModelFactory = dbEmployeeModelFactory;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _employeeService.SearchEmployeesAsync(
                name: null,
                statusId: null,
                designationId : null,
                isCert: false,
                isMvp: false
            );
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
