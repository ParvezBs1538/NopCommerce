using Microsoft.AspNetCore.Mvc;
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
    public class EmployeeController : BasePluginController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeModelFactory _employeeModelFactory;

        public EmployeeController(IEmployeeService employeeService, IEmployeeModelFactory employeeModelFactory)
        {
            _employeeService = employeeService;
            _employeeModelFactory = employeeModelFactory;
        }

        public async Task<IActionResult> List()
        {
            var searchModel = await _employeeModelFactory.PrepareEmployeeSearchModelAsync(new EmployeeSearchModel());
            return View("~/Plugins/WIdgets.Parvez/Views/Developer/List.cshtml", searchModel);
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
            return View("~/Plugins/Widgets.Parvez/Views/Developer/Create.cshtml", model);
        }
    }
}
