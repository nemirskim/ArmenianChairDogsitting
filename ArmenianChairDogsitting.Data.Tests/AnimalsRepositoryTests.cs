using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArmenianChairDogsitting.Data.Tests;

public class AnimalsRepositoryTests
{
    private DbContextOptions<ArmenianChairDogsittingContext> _dbContextOptions;
    private ArmenianChairDogsittingContext _context;
    private AnimalsRepository _sut;

    [SetUp]
    public void SetUp()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ArmenianChairDogsittingContext>()
            .UseInMemoryDatabase(databaseName: $"TestDB")
            .Options;

        _context = new ArmenianChairDogsittingContext(_dbContextOptions);
        _context.Database.EnsureDeleted();
        _sut = new AnimalsRepository(_context);

        _context.Animals.Add(new Animal()
        {
            Id = 1,
            Name = "Bobik",
            Breed = "Ovcharka",
            Age = 6,
            Size = SizeOfAnimal.SmallerThanTenKg,
            RecommendationsForCare = "lubit'",
            ClientId = 2,
            Orders = new List<Order>(),
            IsDeleted = false
        });
        
        _context.Animals.Add(new Animal()
        {
            Id = 1,
            Name = "Warik",
            Breed = "Korgi",
            Age = 3,
            Size = SizeOfAnimal.SmallerThanTenKg,
            RecommendationsForCare = "mnogo kormit'",
            ClientId = 3,
            Orders = new List<Order>(),
            IsDeleted = false
        });

        _context.SaveChanges();
    }
}
