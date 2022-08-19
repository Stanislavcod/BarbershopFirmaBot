using BarbarBot.Common.ModelsDto;
using BarberBot.Model.DataBaseContext;
using BarberBot.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBot.BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDto Get(int id, int cityId);
        IEnumerable<EmployeeDto> Get(string cityName);
        IEnumerable<EmployeeDto> Get();
        EmployeeDto GetEmployee(string name);
    }
}
