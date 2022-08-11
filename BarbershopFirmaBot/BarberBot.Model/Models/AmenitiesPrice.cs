
namespace BarberBot.Model.Models
{
    public class AmenitiesPrice
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int? AmenitiesId { get; set; }
        public Amenities? Amenities { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
