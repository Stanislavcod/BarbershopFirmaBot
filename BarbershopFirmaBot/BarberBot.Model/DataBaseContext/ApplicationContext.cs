using BarberBot.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace BarberBot.Model.DataBaseContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Amenities> Amenities { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City {Id = 1, Name = "Кобрин"},
                new City {Id = 2, Name = "Пинск"}
                );
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "Герман Павловский", Specialization ="Барбер", CityId = 1},
                new Employee { Id = 2, Name = "Нарэк Казарян", Specialization = "Старший барбер", CityId = 1 },
                new Employee { Id = 3, Name = "Юрий Назаров", Specialization = "Барбер" , CityId = 1 },
                new Employee { Id = 4, Name = "Аня Третьяк", Specialization = "Барбер" , CityId = 1 },
                new Employee { Id = 5, Name = "Аня Московенко", Specialization = "Барбер", CityId = 2 },
                new Employee { Id = 6, Name = "Александр Курган", Specialization = "Барбер", CityId = 2 },
                new Employee { Id = 7, Name = "Александ Пешко", Specialization = "Барбер", CityId = 2 },
                new Employee { Id = 8, Name = "Влад Бодиловский", Specialization = "Старший Барбер", CityId = 2 },
                new Employee { Id = 9, Name = "Алексей Седых", Specialization = "Старший Барбер", CityId = 2 }
            );
            modelBuilder.Entity<Amenities>().HasData(
                new Amenities { Id = 1, Title = "Стрижка", Price = 23 },
                new Amenities { Id = 2, Title = "Стрижка машинкой (2 насадки)",Price = 23 },
                new Amenities { Id = 3, Title = "Детская Стрижка (до 10 лет)", Price = 18 },
                new Amenities { Id = 4, Title = "Камуфляж волос (тонирование седины)", Price = 20 },
                new Amenities { Id = 5, Title = "Бритье головы", Price = 23 },
                new Amenities { Id = 6, Title = "Укладка волос", Price = 10 },
                new Amenities { Id = 7, Title = "Коррекция бровей", Price = 5 },
                new Amenities { Id = 8, Title = "Борода и бритье", Price = 20 },
                new Amenities { Id = 9, Title = "Камуфляж бороды", Price = 18 },
                new Amenities { Id = 10, Title = "Борода + Камуфляж", Price = 30 },
                new Amenities { Id = 11, Title = "Комплекс (Стрижка + Борода)", Price = 38 },
                new Amenities { Id = 12, Title = "Комплекс (Стрижка машинкой (2 насадки) + Борода)", Price = 33 },
                new Amenities { Id = 13, Title = "Комплекс (Отец + сын (до 10 лет))", Price = 36 }
                //new Amenities { Id = 14, Title = "Стрижка", Price = 25 },
                //new Amenities { Id = 15, Title = "Стрижка машинкой (2 насадки)", Price = 18 },
                //new Amenities { Id = 16, Title = "Детская Стрижка (до 10 лет)", Price = 20 },
                //new Amenities { Id = 17, Title = "Камуфляж волос (тонирование седины)", Price = 30 },
                //new Amenities { Id = 18, Title = "Бритье головы", Price = 25 },
                //new Amenities { Id = 19, Title = "Коррекция бровей", Price = 5 },
                //new Amenities { Id = 20, Title = "Камуфляж бороды", Price = 23 },
                //new Amenities { Id = 21, Title = "Борода + Камуфляж", Price = 33 },
                //new Amenities { Id = 22, Title = "Комплекс (Стрижка + Борода)", Price = 40 },
                //new Amenities { Id = 23, Title = "Комплекс (Стрижка машинкой (2 насадки) + Борода)", Price = 30 },
                //new Amenities { Id = 24, Title = "Комплекс (Отец + сын (до 10 лет))", Price = 40 },
                //new Amenities { Id = 25, Title = "Коррекция воском", Price = 5 }
                );
        }
    }
}
