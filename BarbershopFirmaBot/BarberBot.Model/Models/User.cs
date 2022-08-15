
namespace BarberBot.Model.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public List<Order> Order { get; set; } = new();
        
        public int? CityId { get; set; }
        public City? City { get; set; }
    }
}
