using exercise.pizzashopapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Numerics;

namespace exercise.pizzashopapi.Data
{
    public class DataContext : DbContext
    {
        private string connectionString;
        public DataContext()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString") !;

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.LogTo(message => Debug.WriteLine(message)); 

            //set primary of order?

            //seed data?

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Pizza>().HasMany(k => k.Orders);
            modelBuilder.Entity<Customer>().HasMany(k => k.Orders);
            modelBuilder.Entity<Order>().HasKey(dp => new { dp.CustomerId, dp.PizzaId });
          


            
            modelBuilder.Entity<Pizza>().HasData(
               new { Id = 1, Name = "Margherita", Price = 100.00M}
            );

            modelBuilder.Entity<Customer>().HasData(

               new { Id = 1, Name = "Mayra Mahamud" }

           );

            modelBuilder.Entity<Order>().HasData(
                new Order() { OrderId = 1, CustomerId = 1, PizzaId = 1 }


           );
        }







        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
