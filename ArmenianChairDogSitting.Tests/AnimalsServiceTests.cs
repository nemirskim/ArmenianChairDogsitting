using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;
using Moq;

namespace ArmenianChairDogsitting.Business.Tests;

public class AnimalsServiceTests
{
    private Mock<IAnimalsRepository> _animalsRepository;
    private AnimalsService _sut;

    [SetUp]
    public void Setup()
    {
        _animalsRepository = new Mock<IAnimalsRepository>();
        _sut = new AnimalsService(_animalsRepository.Object);
    }

    [Test]
    public void AddAnimal_WhenValidationPassed_ThenReturnIdOfAddedAnimal()
    {
        //given
        var expectedId = 1;

        var dog = new Animal()
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
        };

        _animalsRepository.Setup(a => a.AddAnimal(dog))
             .Returns(expectedId);

        //when
        var actual = _sut.AddAnimal(dog);

        //then
        Assert.AreEqual(actual, expectedId);
        _animalsRepository.Verify(a => a.AddAnimal(dog), Times.Once);
    }

    [Test]
    public void GetAnimalById_WhenAnimalExist_ThenReturnAnimal()
    {
        //given
        var expectedDog = new Animal()
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
        };

        _animalsRepository
            .Setup(a => a.GetAnimalById(expectedDog.Id))
            .Returns(expectedDog);

        //when
        var actualDog = _sut.GetAnimalById(1);

        //then
        Assert.AreEqual(actualDog.Id, expectedDog.Id);
        Assert.AreEqual(actualDog.Name, expectedDog.Name);
        Assert.AreEqual(actualDog.Breed, expectedDog.Breed);
        Assert.AreEqual(actualDog.Age, expectedDog.Age);
        Assert.AreEqual(actualDog.Size, expectedDog.Size);
        Assert.AreEqual(actualDog.RecommendationsForCare, expectedDog.RecommendationsForCare);

        _animalsRepository.Verify(a => a.GetAnimalById(expectedDog.Id), Times.Once);
    }

    [Test]
    public void GetAnimalsByClient_WhenIdAnimalExist_ThenReturnListAnimals()
    {
        //given
        int clientId = 2;

        List<Animal> expectedDogs = new List<Animal>
        {
            new Animal()
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
            },
            new Animal()
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
            }           
        };

        _animalsRepository
            .Setup(a => a.GetAllAnimalsByClient(clientId))
            .Returns(expectedDogs);

        //when
        var actual = _sut.GetAllAnimalsByClient(clientId);

        //then
        Assert.True(actual is not null);
        Assert.AreEqual(actual.Count, expectedDogs.Count);
        Assert.True(actual is List<Animal>);
    }

    [Test]
    public void UpdateAnimal_WhenValidationPassed_ThenUpdateProperties()
    {
        //given
        var dog = new Animal()
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
        };

        var dogForUpdate = new Animal()
        {
            Id = 5,
            Name = "Pupsik",
            Breed = "Korgi",
            Age = 5,
            Size = SizeOfAnimal.SmallerThanTenKg,
            RecommendationsForCare = "mnogo kormit' i celovat'",
            ClientId = 2,
            Orders = new List<Order>(),
            IsDeleted = true
        };

        _animalsRepository.Setup(a => a.GetAnimalById(dog.Id)).Returns(dog);

        //when
        _sut.UpdateAnimal(dogForUpdate, dog.Id);

        //then
        var actual = _sut.GetAnimalById(dog.Id);

        _animalsRepository.Verify(c => c.GetAnimalById(dog.Id), Times.Exactly(2));
        _animalsRepository.Verify(c => c.UpdateAnimal(It.Is<Animal>(a =>
        a.IsDeleted == dog.IsDeleted &&
        a.Name == dogForUpdate.Name &&
        a.Age == dogForUpdate.Age &&
        a.Size == dogForUpdate.Size &&
        a.RecommendationsForCare == dogForUpdate.RecommendationsForCare &&
        a.Id == dog.Id &&
        a.ClientId == dog.ClientId &&
        a.Orders == dog.Orders
        )), Times.Once);
    }

    [Test]
    public void UpdateAnimal_WhenAnimalIsNotExist_ThenNotFoundException()
    {
        //given
        int dogId = 1;

        var dog = new Animal()
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
        };

        var dogForUpdate = new Animal()
        {
            Id = 5,
            Name = "Pupsik",
            Breed = "Korgi",
            Age = 5,
            Size = SizeOfAnimal.SmallerThanTenKg,
            RecommendationsForCare = "mnogo kormit' i celovat'",
            ClientId = 2,
            Orders = new List<Order>(),
            IsDeleted = true
        };

        _animalsRepository.Setup(a => a.GetAnimalById(dog.Id)).Returns(dog);

        //when

        //then
        Assert.Throws<NotFoundException>(() => _sut.UpdateAnimal(dogForUpdate, dogId));
    }

    [Test]
    public void RemoveOrRestoreById_WhenAnimalIsNotDeleted_ThenDeleteAnimal()
    {
        ///given
        var expectedDog = new Animal()
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
        };

        _animalsRepository.Setup(a => a.GetAnimalById(expectedDog.Id)).Returns(expectedDog);

        //when
        _sut.RemoveOrRestoreAnimal(expectedDog.Id, true);

        //then
        var allDogs = _sut.GetAllAnimalsByClient(2);
        var actualDog = _sut.GetAnimalById(expectedDog.Id);

        Assert.True(allDogs is null);
        Assert.True(actualDog.IsDeleted);

        _animalsRepository.Verify(a => a.GetAnimalById(expectedDog.Id), Times.Exactly(2));
        _animalsRepository.Verify(a => a.GetAllAnimalsByClient(2), Times.Once);
    }

    [Test]
    public void RemoveOrRestoreById_WhenAnimalIsDeleted_ThenRestoreAnimal()
    {
        ///given
        var expectedDog = new Animal()
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
        };

        _animalsRepository.Setup(a => a.GetAnimalById(expectedDog.Id)).Returns(expectedDog);
        _animalsRepository.Setup(a => a.GetAllAnimalsByClient(2)).Returns(new List<Animal> { expectedDog });

        //when
        _sut.RemoveOrRestoreAnimal(expectedDog.Id, false);

        //then
        var allDogs = _sut.GetAllAnimalsByClient(2);
        var actualDog = _sut.GetAnimalById(expectedDog.Id);

        Assert.True(allDogs is not null);
        Assert.False(actualDog.IsDeleted);

        _animalsRepository.Verify(a => a.GetAnimalById(expectedDog.Id), Times.Exactly(2));
        _animalsRepository.Verify(a => a.GetAllAnimalsByClient(2), Times.Once);
    }

    [Test]
    public void RemoveOrRestoreById_WhenAnimalIsNotExist_ThenNotFoundException()
    {
        //given
        int dogId = 1;

        var expectedDog = new Animal()
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
        };

        _animalsRepository.Setup(a => a.GetAnimalById(expectedDog.Id)).Returns(expectedDog);

        //when

        //then
        Assert.Throws<NotFoundException>(() => _sut.RemoveOrRestoreAnimal(dogId, true));
    }
}
