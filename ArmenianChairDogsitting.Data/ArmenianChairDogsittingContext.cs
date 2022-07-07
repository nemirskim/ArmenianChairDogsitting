using ArmenianChairDogsitting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArmenianChairDogsitting.Data
{
    public class ArmenianChairDogsittingContext : DbContext
    {
        public DbSet<Sitter> Sitters { get; set; }

        public ArmenianChairDogsittingContext(DbContextOptions<ArmenianChairDogsittingContext> options)
                : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sitter>(entity =>
            {
                entity.ToTable(nameof(Sitter));
                entity.HasKey(e => e.Id);

                
            });
        }
    }
}