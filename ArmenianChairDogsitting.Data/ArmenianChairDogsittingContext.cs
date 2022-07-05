using Microsoft.EntityFrameworkCore;
using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data
{
    public class ArmenianChairDogsittingContext : DbContext 
    {
        public DbSet<Client> Clients { get; set; }

        public ArmenianChairDogsittingContext(DbContextOptions<ArmenianChairDogsittingContext> options)
        : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable(nameof(Client));
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).HasMaxLength(30);
                entity.Property(e => e.LastName).HasMaxLength(50);
            });
           
        }
    }
}