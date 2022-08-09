
using AutoMapper;
using BarbarBot.Common.ModelsDto;
using BarberBot.BusinessLogic.Interfaces;
using BarberBot.Model.DataBaseContext;
using BarberBot.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace BarberBot.BusinessLogic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        public EmployeeService(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }
        public IEnumerable<EmployeeDto> Get(string cityName)
        {
            var employee = _applicationContext.Employees.ToList().Where(x=> x.City.Name == cityName);
            var employeeDto = _mapper.Map<List<EmployeeDto>>(employee);
            return employeeDto;
        }
        public EmployeeDto Get(int id, int cityId)
        {
            var employee = _applicationContext.Employees.FirstOrDefault(x => x.Id == id && x.CityId == cityId);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }
        public IEnumerable<EmployeeDto> Get()
        {
            var employee = _applicationContext.Employees.ToList();
            var employeeDto = _mapper.Map<List<EmployeeDto>>(employee);
            return employeeDto;
        }
    }
}
