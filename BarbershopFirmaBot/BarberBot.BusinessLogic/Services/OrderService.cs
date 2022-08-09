using AutoMapper;
using BarbarBot.Common.ModelsDto;
using BarberBot.BusinessLogic.Interfaces;
using BarberBot.Model.DataBaseContext;
using BarberBot.Model.Models;

namespace BarberBot.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public OrderService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }
        public void Create(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}
