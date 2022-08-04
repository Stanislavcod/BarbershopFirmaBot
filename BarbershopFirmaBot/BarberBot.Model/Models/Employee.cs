
namespace BarberBot.Model.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public string Specialization { get; set; } = string.Empty;
        public string? Photo { get; set; } = string.Empty;

        public List<Order> Orders = new();
        public List<Amenities> Amenities = new();
    }
}
