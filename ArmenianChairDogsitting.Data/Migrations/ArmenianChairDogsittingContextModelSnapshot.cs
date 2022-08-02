﻿// <auto-generated />
using System;
using ArmenianChairDogsitting.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ArmenianChairDogsitting.Data.Migrations
{
    [DbContext(typeof(ArmenianChairDogsittingContext))]
    partial class ArmenianChairDogsittingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AnimalOrder", b =>
                {
                    b.Property<int>("AnimalsId")
                        .HasColumnType("int");

                    b.Property<int>("OrdersId")
                        .HasColumnType("int");

                    b.HasKey("AnimalsId", "OrdersId");

                    b.HasIndex("OrdersId");

                    b.ToTable("AnimalOrder");
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Animal", (string)null);
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeUpdated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("OrderId");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.District", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("District", (string)null);
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SitterId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("SitterId");

                    b.ToTable("Order", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("Order");
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.PriceCatalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Service")
                        .HasColumnType("int");

                    b.Property<int>("SitterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SitterId");

                    b.ToTable("PriceCatalog", (string)null);
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.Sitter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Experience")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sitter", (string)null);
                });

            modelBuilder.Entity("DistrictSitter", b =>
                {
                    b.Property<int>("DistrictsId")
                        .HasColumnType("int");

                    b.Property<int>("SittersId")
                        .HasColumnType("int");

                    b.HasKey("DistrictsId", "SittersId");

                    b.HasIndex("SittersId");

                    b.ToTable("DistrictSitter");
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.OrderDailySitting", b =>
                {
                    b.HasBaseType("ArmenianChairDogsitting.Data.Entities.Order");

                    b.Property<int>("DayQuantity")
                        .HasColumnType("int");

                    b.Property<int>("WalkQuantity")
                        .HasColumnType("int");

                    b.ToTable("Order", (string)null);

                    b.HasDiscriminator().HasValue("OrderDailySitting");
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.OrderOverexpose", b =>
                {
                    b.HasBaseType("ArmenianChairDogsitting.Data.Entities.Order");

                    b.Property<int>("DayQuantity")
                        .HasColumnType("int")
                        .HasColumnName("OrderOverexpose_DayQuantity");

                    b.Property<int>("WalkPerDayQuantity")
                        .HasColumnType("int");

                    b.ToTable("Order", (string)null);

                    b.HasDiscriminator().HasValue("OrderOverexpose");
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.OrderSittingForDay", b =>
                {
                    b.HasBaseType("ArmenianChairDogsitting.Data.Entities.Order");

                    b.Property<int>("HourQuantity")
                        .HasColumnType("int");

                    b.Property<int>("VisitQuantity")
                        .HasColumnType("int");

                    b.Property<int>("WalkQuantity")
                        .HasColumnType("int")
                        .HasColumnName("OrderSittingForDay_WalkQuantity");

                    b.ToTable("Order", (string)null);

                    b.HasDiscriminator().HasValue("OrderSittingForDay");
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.OrderWalk", b =>
                {
                    b.HasBaseType("ArmenianChairDogsitting.Data.Entities.Order");

                    b.Property<bool>("IsTrial")
                        .HasColumnType("bit");

                    b.Property<int>("WalkQuantity")
                        .HasColumnType("int")
                        .HasColumnName("OrderWalk_WalkQuantity");

                    b.ToTable("Order", (string)null);

                    b.HasDiscriminator().HasValue("OrderWalk");
                });

            modelBuilder.Entity("AnimalOrder", b =>
                {
                    b.HasOne("ArmenianChairDogsitting.Data.Entities.Animal", null)
                        .WithMany()
                        .HasForeignKey("AnimalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArmenianChairDogsitting.Data.Entities.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.Animal", b =>
                {
                    b.HasOne("ArmenianChairDogsitting.Data.Entities.Client", null)
                        .WithMany("Dogs")
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.Comment", b =>
                {
                    b.HasOne("ArmenianChairDogsitting.Data.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArmenianChairDogsitting.Data.Entities.Order", "Order")
                        .WithMany("Comments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.Order", b =>
                {
                    b.HasOne("ArmenianChairDogsitting.Data.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArmenianChairDogsitting.Data.Entities.Sitter", "Sitter")
                        .WithMany("Orders")
                        .HasForeignKey("SitterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Sitter");
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.PriceCatalog", b =>
                {
                    b.HasOne("ArmenianChairDogsitting.Data.Entities.Sitter", "Sitter")
                        .WithMany("PriceCatalog")
                        .HasForeignKey("SitterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sitter");
                });

            modelBuilder.Entity("DistrictSitter", b =>
                {
                    b.HasOne("ArmenianChairDogsitting.Data.Entities.District", null)
                        .WithMany()
                        .HasForeignKey("DistrictsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArmenianChairDogsitting.Data.Entities.Sitter", null)
                        .WithMany()
                        .HasForeignKey("SittersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.Client", b =>
                {
                    b.Navigation("Dogs");
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.Order", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("ArmenianChairDogsitting.Data.Entities.Sitter", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("PriceCatalog");
                });
#pragma warning restore 612, 618
        }
    }
}
