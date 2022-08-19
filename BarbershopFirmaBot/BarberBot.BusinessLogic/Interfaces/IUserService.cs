using BarbarBot.Common.ModelsDto;

namespace BarberBot.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        void Create(UserDto userDto);
        void Delete(int id);
        UserDto Get(string name);
    }
}
