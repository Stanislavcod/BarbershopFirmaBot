using BarberBot.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace BarberBot.Model.DataBaseContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Amenities> Amenities { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
    }
}
