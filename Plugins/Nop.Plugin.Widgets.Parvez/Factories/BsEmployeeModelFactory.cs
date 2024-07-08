﻿using Nop.Plugin.Widgets.Parvez.Domain;
using Nop.Plugin.Widgets.Parvez.Models;

namespace Nop.Plugin.Widgets.Parvez.Factories
{
    public class BsEmployeeModelFactory : IBsEmployeeModelFactory
    {
        public async Task<IList<EmployeePublicModel>> PrepareEmployeeListModelFactoryAsync(IList<BsEmployee> employees)
        {
            var model = new List<EmployeePublicModel>();

            foreach (var employee in employees)
                model.Add(await PrepareEmployeeModelFactoryAsync(employee));

            return model;
        }

        public async Task<EmployeePublicModel> PrepareEmployeeModelFactoryAsync(BsEmployee employee)
        {
            return new EmployeePublicModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                IsMVP = employee.IsMVP,
                IsCertified = employee.IsCertified,
            };
        }
    }
}
