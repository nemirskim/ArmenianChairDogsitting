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
}
