
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArmenianChairDogsitting.Data.Tests;

public class ScheduleRepositoryTests
{
    private DbContextOptions<ArmenianChairDogsittingContext> _dbContextOptions;
    private ArmenianChairDogsittingContext _context;
    private ScheduleRepository _sut;

    [SetUp]
    public void Setup()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ArmenianChairDogsittingContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb")
            .Options;

        _context = new ArmenianChairDogsittingContext(_dbContextOptions);
        _context.Database.EnsureDeleted();

        _sut = new ScheduleRepository(_context);

        _context.Schedules.Add(new Schedule()
        {
            Start = DateTime.Now,
            End = DateTime.Now.AddHours(2),
            Day = DayOfWeek.Sunday,
            Sitter = new Sitter
            {
                Id = 3,
                Name = "Ludmila",
                LastName = "Transformatorovna",
                Phone = "89375648354",
                Email = "trans@t.com",
                Password = "123456789",
                Age = 19,
                Experience = 3,
                Sex = Sex.Female,
                Description = ""
            }
    });

        _context.Schedules.Add(new Schedule()
        {
            Start = new DateTime(2022, 08, 11, 11, 0, 0),
            End = new DateTime(2022, 08, 11, 13, 0, 0),
            Day = DayOfWeek.Sunday,
            Sitter = new Sitter()
            {
                Id = 1,
                Name = "Misha",
                LastName = "Percev",
                Phone = "89657438654",
                Email = "per@per.com",
                Password = "123456789",
                Age = 47,
                Experience = 2,
                Sex = Sex.Male,
                Description = "",
                PriceCatalog = new List<PriceCatalog>(),
                Orders = new List<Order>(),
                IsDeleted = false
            }
        });

        _context.Schedules.Add(new Schedule()
        {
            Start = DateTime.Now,
            End = DateTime.Now.AddHours(2),
            Day = DayOfWeek.Sunday,
            Sitter = new Sitter()
            {
                Id = 2,
                Name = "Svetlana",
                LastName = "Nefiltrovovna",
                Phone = "89991116116",
                Email = "svetloe@pi.com",
                Password = "123456789",
                Age = 30,
                Experience = 12,
                Sex = Sex.Female,
                Description = "",
                PriceCatalog = new List<PriceCatalog>(),
                Orders = new List<Order>(),
                IsDeleted = false
            }
        });

        _context.SaveChanges();
    }

    [Test]
    public void GetScheduleById_WhenScheduleIsExist_ThenReturnWorkTime()
    {
        //given
        var expectedSitter = new Schedule
        {
            Id = 2,
            Start = new DateTime(2022, 08, 11, 11, 0, 0),
            End = new DateTime(2022, 08, 11, 13, 0, 0),
            Day = DayOfWeek.Sunday,
            Sitter = new Sitter { Id = 2 }
        };

        //when
        var actualSitter = _sut.GetScheduleById(2);

        //then
        Assert.AreEqual(expectedSitter.Id, actualSitter.Id);
        Assert.AreEqual(expectedSitter.Start, actualSitter.Start);
        Assert.AreEqual(expectedSitter.End, actualSitter.End);
        Assert.AreEqual(expectedSitter.Day, actualSitter.Day);
    }
}
