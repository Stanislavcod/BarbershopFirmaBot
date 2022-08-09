

using BarbarBot.Common.ModelsDto;

namespace BarberBot.BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        void Create(OrderDto order);
        void Delete(int id);
    }
}
