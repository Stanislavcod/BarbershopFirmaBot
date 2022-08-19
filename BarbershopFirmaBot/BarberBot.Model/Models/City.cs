
namespace BarberBot.Model.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<User> Users { get; set; } = new();
        public List<Employee> Employees { get; set; } = new();
    }
}
