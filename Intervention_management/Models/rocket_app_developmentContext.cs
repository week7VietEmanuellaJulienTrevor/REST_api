using Microsoft.EntityFrameworkCore;

namespace intervention_management.Models
{
    public class Rocket_app_developmentContext : DbContext
    {
        public Rocket_app_developmentContext(DbContextOptions<Rocket_app_developmentContext> options)
            : base(options)
        {
        }

        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Elevator> Elevators { get; set; }

        
    }
}