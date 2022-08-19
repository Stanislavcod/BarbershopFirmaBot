using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarBot.Common.ModelsDto
{
    public class OrderDto
    {
        public DateTime DateOfRecording { get; set; }
        public UserDto UserDto { get; set; }
        public EmployeeDto EmployeeDto { get; set; }
        public AmenitiesDto AmenitiesDto { get; set; }
    }
}
