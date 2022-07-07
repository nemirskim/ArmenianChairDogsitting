using ArmenianChairDogsitting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArmenianChairDogsitting.Data
{
    public class ArmenianChairDogsittingContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ArmenianChairDogsittingContext(DbContextOptions<ArmenianChairDogsittingContext> options)
                : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderOverexpose>()
                .HasBaseType<Order>()
                .ToTable(nameof(Order));

            modelBuilder.Entity<OrderDailySitting>()
                .HasBaseType<Order>()
                .ToTable(nameof(Order));

            modelBuilder.Entity<OrderSittingForDay>()
                .HasBaseType<Order>()
                .ToTable(nameof(Order));

            modelBuilder.Entity<OrderWalk>()
                .HasBaseType<Order>()
                .ToTable(nameof(Order));

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.ToTable(nameof(Animal));
                entity.HasKey(e => e.Id);

                entity
                    .HasMany(o => o.Orders)
                    .WithMany(a => a.Animals);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable(nameof(Comment));
                entity.HasKey(e => e.Id);

                entity
                    .HasOne(o => o.Order)
                    .WithMany(c => c.Comments);
            });
        }
    }
}