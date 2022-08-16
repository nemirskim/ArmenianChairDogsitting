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
    public void Setup()
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
            Id = 2,
            Name = "Warik",
            Breed = "Korgi",
            Age = 3,
            Size = SizeOfAnimal.SmallerThanTenKg,
            RecommendationsForCare = "mnogo kormit'",
            ClientId = 2,
            Orders = new List<Order>(),
            IsDeleted = false
        });

        _context.SaveChanges();
    }

    [Test]
    public void AddAnimal_WhenNewAnimalIsCorrect_ThenShouldAddExpectedAnimal()
    {
        //given 
        var dog = new Animal()
        {
            Id = 3,
            Name = "Kysaka",
            Breed = "Siba iny",
            Age = 4,
            Size = SizeOfAnimal.SmallerThanTenKg,
            RecommendationsForCare = "lubit'",
            ClientId = 2,
            Orders = new List<Order>(),
            IsDeleted = false
        };

        //when 
        _sut.AddAnimal(dog);

        //then 
        var expected = _context.Animals.FirstOrDefault(a => a.Id == dog.Id);
        Assert.AreEqual(expected.Id, dog.Id);
    }

    [Test]
    public void GetAnimalById_WhenIdIsCorrect_ShouldReturnExpectedAnimal()
    {
        //given in setup 

        //when 
        var dog = _sut.GetAnimalById(2);

        //then 
        Assert.AreEqual(2, dog.Id);
        Assert.NotNull(dog);
    }

    [Test]
    public void GetAllAnimalsByClient_ShouldReturnAllOfExpectedAnimals()
    {
        //given 
        var expectedCountOfDogs = 2;

        //when 
        var dog = _sut.GetAllAnimalsByClient(2);

        //then 
        Assert.AreEqual(expectedCountOfDogs, dog.Count);
    }

    [Test]
    public void UpdateAnimal_WhenCorrectIdIsPassed_ThenUpdateAnimalApplied()
    {
        //given 
        var id = 2;
        var actual = _context.Animals.FirstOrDefault(a => a.Id == id);
        actual.Breed = "Wpic";
        _context.SaveChanges();

        //when
        _sut.UpdateAnimal(actual);

        //then 
        Assert.AreEqual(id, actual.Id);
        Assert.AreEqual("Wpic", actual.Breed);
    }

    [Test]
    public void RemoveAnimal_CorrectIdIsPassed_ThenSoftDeleteApplied()
    {
        //given 
        var id = 1;
        var dog = _context.Animals.FirstOrDefault(a => a.Id == id);
        dog.IsDeleted = true;

        //when 
        _sut.RemoveOrRestoreAnimal(dog);

        //then 
        Assert.True(dog.IsDeleted);
    }

    [Test]
    public void RestoreAnimal_WhenCorrectIdIsPassed_ThenRestoreApplied()
    {
        //given 
        var id = 2;
        var dog = _context.Animals.FirstOrDefault(a => a.Id == id);
        dog.IsDeleted = false;

        //when 
        _sut.RemoveOrRestoreAnimal(dog);

        //then 
        Assert.False(dog.IsDeleted);
    }
}
