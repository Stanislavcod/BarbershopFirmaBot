
using AutoMapper;
using BarbarBot.Common.ModelsDto;
using BarberBot.BusinessLogic.Interfaces;
using BarberBot.Model.DataBaseContext;
using BarberBot.Model.Models;

namespace BarberBot.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public UserService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Create(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
