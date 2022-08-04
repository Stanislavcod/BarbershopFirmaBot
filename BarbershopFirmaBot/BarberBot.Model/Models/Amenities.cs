
namespace BarberBot.Model.Models
{
    public class Amenities
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Employee> Employees { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
    }
}
