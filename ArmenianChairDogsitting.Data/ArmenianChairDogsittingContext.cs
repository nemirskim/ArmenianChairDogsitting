﻿using ArmenianChairDogsitting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArmenianChairDogsitting.Data
{
    public class ArmenianChairDogsittingContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Animal> Animals { get; set; }

        public ArmenianChairDogsittingContext(DbContextOptions<ArmenianChairDogsittingContext> options)
                : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable(nameof(Order));
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.ToTable(nameof(Animal));
                entity.HasKey(e => e.Id);

                entity
                    .HasOne(o => o.Order)
                    .WithMany(a => a.Animals);
            });
        }
    }
}