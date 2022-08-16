
using ArmenianChairDogsitting.Business.Services;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories.Interfaces;
using Moq;

namespace ArmenianChairDogsitting.Business.Tests;

public class ScheduleServiceTeste
{
    private Mock<IScheduleRepository> _sheduleRepository;
    private ScheduleService _sut;
    private Mock<ISittersRepository> _sitterRepository;

    [SetUp]
    public void Setup()
    {
        _sheduleRepository = new Mock<IScheduleRepository>();
        _sitterRepository = new Mock<ISittersRepository>();
        _sut = new ScheduleService(_sitterRepository.Object, _sheduleRepository.Object);
    }

    [Test]
    public void AddWorkTime_WhenValidationPassed_ThenReturnIdOfAddedWorkTime()
    {
        //given
        var expectedId = 1;

        var workTime = new Schedule()
        {
            Day = DayOfWeek.Monday,
            Start = DateTime.Now,
            End = DateTime.Now.AddTicks(2),
        };

        var sitter = new Sitter()
        {
            Id = 2,
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Age = 27,
            Experience = 7,
            Sex = Sex.Male,
            Description = "",
            PriceCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false
        };

        _sitterRepository.Setup(c => c.GetById(sitter.Id))
             .Returns(sitter);
        _sheduleRepository.Setup(c => c.AddWorkTime(workTime))
             .Returns(expectedId);

        //when
        var actual = _sut.AddWorkTime(workTime, sitter.Id);


        //then

        Assert.AreEqual(actual, expectedId);
        _sitterRepository.Verify(c => c.GetById(sitter.Id), Times.Once);
        _sheduleRepository.Verify(c => c.AddWorkTime(It.Is<Schedule>(sc =>
        sc.End == workTime.End &&
        sc.Start == workTime.Start &&
        sc.Day == workTime.Day
        )), Times.Once);
    }

    [Test]
    public void GetSitterSchedule_WhenExist_ThenReturnSchedule()
    {
        //given
        var sitter = new Sitter()
        {
            Id = 2,
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Age = 27,
            Experience = 7,
            Sex = Sex.Male,
            Description = "",
            PriceCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false,
            Schedules = new List<Schedule>
            {
                new Schedule()
                {
                    Day = DayOfWeek.Monday,
                    Start = DateTime.Now,
                    End = DateTime.Now.AddTicks(2),
                },
                new Schedule()
                {
                    Day = DayOfWeek.Monday,
                    Start = DateTime.Now.AddTicks(2),
                    End = DateTime.Now.AddTicks(4),
                },
                new Schedule()
                {
                    Day = DayOfWeek.Monday,
                    Start = DateTime.Now.AddTicks(4),
                    End = DateTime.Now.AddTicks(6),
                }
            }
        };

        _sitterRepository
            .Setup(x => x.GetById(sitter.Id))
            .Returns(sitter);

        //when
        var actual = _sut.GetSitterSchedule(sitter.Id);

        //then
        Assert.True(actual is not null);
        Assert.AreEqual(actual.Count, sitter.Schedules.Count);
        Assert.True(actual is List<Schedule>);
    }

    [Test]
    public void RemoveOrRestoreById_WhenSitterIsNotDeleted_ThenDeleteSitter()
    {
        //given
        var expectedSitter = new Sitter()
        {
            Id = 10,
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Age = 27,
            Experience = 7,
            Sex = Sex.Male,
            Description = "",
            PriceCatalog = new List<PriceCatalog>(),
            Orders = new List<Order>(),
            IsDeleted = false, 
            Schedules = new List<Schedule>
            {
                new Schedule()
                {
                    Id = 1,
                    Day = DayOfWeek.Monday,
                    Start = DateTime.Now,
                    End = DateTime.Now.AddTicks(2),
                },
                new Schedule()
                {
                    Id = 2,
                    Day = DayOfWeek.Monday,
                    Start = DateTime.Now.AddTicks(2),
                    End = DateTime.Now.AddTicks(4),
                },
                new Schedule()
                {
                    Id = 3,
                    Day = DayOfWeek.Monday,
                    Start = DateTime.Now.AddTicks(4),
                    End = DateTime.Now.AddTicks(6),
                }
            }
        };

        var expectedWorkTime = new Schedule()
        {
            Id = 3,
            Day = DayOfWeek.Monday,
            Start = DateTime.Now.AddTicks(4),
            End = DateTime.Now.AddTicks(6),
        };

        _sitterRepository.Setup(o => o.GetById(expectedSitter.Id)).Returns(expectedSitter);
        _sheduleRepository.Setup(o => o.GetScheduleById(expectedSitter.Schedules[2].Id)).Returns(expectedSitter.Schedules[2]);

        //when
        _sut.RemoveWorkTime(expectedSitter.Id, expectedSitter.Schedules[2].Id);


        //then

        var allSitters = _sut.GetSitterSchedule(expectedSitter.Id);

        Assert.True(allSitters.Count == 2);

        _sitterRepository.Verify(c => c.GetById(expectedSitter.Id), Times.Exactly(2));
    }
}
