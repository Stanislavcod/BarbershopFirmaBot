﻿
using AutoMapper;
using BarbarBot.Common.ModelsDto;
using BarberBot.BusinessLogic.Interfaces;
using BarberBot.Model.DataBaseContext;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<EmployeeDto> Get()
        {
            var employee = _applicationContext.Employees.AsNoTracking().ToList();
            var employeeDto = _mapper.Map<List<EmployeeDto>>(employee);
            return employeeDto;
        }
        public EmployeeDto Get(int id)
        {
            var employee = _applicationContext.Employees.FirstOrDefault(x => x.Id == id);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }
    }
}
