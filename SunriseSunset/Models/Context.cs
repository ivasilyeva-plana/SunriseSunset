using System.Data.Entity;

namespace SunriseSunset.Models
{
    public class Context : DbContext
    {
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>()
                .HasIndex(p => p.Key)
                .IsUnique(true);
        }
    }
}