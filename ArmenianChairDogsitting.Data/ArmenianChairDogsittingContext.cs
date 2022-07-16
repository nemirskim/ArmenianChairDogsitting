using Microsoft.EntityFrameworkCore;
using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data
{
    public class ArmenianChairDogsittingContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Sitter> Sitters { get; set; }

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
           
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable(nameof(Order));
                entity.HasKey(e => e.Id);
            });
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
            modelBuilder.Entity<Sitter>(entity =>
            {
                entity.ToTable(nameof(Sitter));
                entity.HasKey(e => e.Id);


            });

            modelBuilder.Entity<PriceCatalog>(entity =>
            {
                entity.ToTable(nameof(PriceCatalog));
                entity.HasKey(e => e.Id);
            });
        }
    }
}