using Nop.Plugin.Misc.NopStationTeam.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.NopStationTeam.Services;

public interface IEmployeeService
{
    void Log(Employee employee);
}
