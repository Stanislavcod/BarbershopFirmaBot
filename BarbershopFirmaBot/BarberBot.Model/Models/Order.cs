
namespace BarberBot.Model.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateOfRecording { get; set; }
        
        
        public int AmenitiesId { get; set; }
        public Amenities? Amenities { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
