using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArmenianChairDogsitting.Data.Tests
{
    public class SitterRepositoryTests
    {
        private DbContextOptions<ArmenianChairDogsittingContext> _dbContextOptions;
        private ArmenianChairDogsittingContext _context;
        private SitterRepository _sut;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ArmenianChairDogsittingContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb")
                .Options;

            _context = new ArmenianChairDogsittingContext(_dbContextOptions);
            _context.Database.EnsureDeleted();

            _sut = new SitterRepository(_context);

            _context.Sitters.Add(new Sitter()
            {
                Name = "Alex",
                LastName = "Pistoletov",
                Phone = "89991116116",
                Email = "pistol@pi.com",
                Password = "123456789",
                Age = 27,
                Experience = 7,
                Sex = Sex.Male,
                Description = "",
                PricesCatalog = new List<PriceCatalog>(),
                Orders = new List<Order>(),
                IsDeleted = false
            });

            _context.Sitters.Add(new Sitter()
            {
                Name = "Misha",
                LastName = "Percev",
                Phone = "89657438654",
                Email = "per@per.com",
                Password = "123456789",
                Age = 47,
                Experience = 2,
                Sex = Sex.Male,
                Description = "",
                PricesCatalog = new List<PriceCatalog>(),
                Orders = new List<Order>(),
                IsDeleted = false
            });

            _context.Sitters.Add(new Sitter()
            {
                Name = "Ludmila",
                LastName = "Transformatorovna",
                Phone = "89375648354",
                Email = "trans@t.com",
                Password = "123456789",
                Age = 19,
                Experience = 3,
                Sex = Sex.Female,
                Description = "",
                PricesCatalog =
                {
                    new PriceCatalog
                    {
                        Id = 1,
                        Service = new Service { Id = ServiceEnum.SittingForDay},
                        Price = 600,
                        Sitter = new Sitter{Name = "Ludmila"}
                    }
                },
                Orders = new List<Order>(),
                IsDeleted = false
            });

            _context.Sitters.Add(new Sitter()
            {
                Name = "Svetlana",
                LastName = "Nefiltrovovna",
                Phone = "89991116116",
                Email = "svetloe@pi.com",
                Password = "123456789",
                Age = 30,
                Experience = 12,
                Sex = Sex.Female,
                Description = "",
                PricesCatalog = new List<PriceCatalog>(),
                Orders = new List<Order>(),
                IsDeleted = true
            });

            _context.SaveChanges();
        }

        [Test]
        public void GetById_WhenValidTitlePassed_ThenReturnSitter()
        {
            //given
            var expectedSitter = new Sitter
            {
                Id = 3,
                Name = "Ludmila",
                LastName = "Transformatorovna",
                Phone = "89375648354",
                Email = "trans@t.com",
                Password = "123456789",
                Age = 19,
                Experience = 3,
                Sex = Sex.Female
            };

            //when
            var actualSitter = _sut.GetById(3);

            //then
            Assert.True(expectedSitter.Id == actualSitter.Id);
            Assert.True(expectedSitter.Name == actualSitter.Name);
            Assert.True(expectedSitter.LastName == actualSitter.LastName);
            Assert.True(expectedSitter.Email == actualSitter.Email);
        }

        [Test]
        public void GetSitters_WhenCalled_ReturnsAllSitters()
        {
            //given
            int expectedCount = 4;

            //when
            var actualSitters = _sut.GetSitters();

            //then

            Assert.NotNull(actualSitters);
            Assert.True(actualSitters.Count == expectedCount);
        }

        [Test]
        public void RemoveOrRestoreById_WhenIsDeletedEqualsFalse_ThenSoftDeleted()
        {
            //given
            int sitterId = 2;

            //when
            _sut.RemoveOrRestoreById(sitterId);

            //then
            var actualSitter = _sut.GetById(sitterId);

            Assert.NotNull(actualSitter);
            Assert.True(actualSitter.IsDeleted);
        }

        [Test]
        public void RemoveOrRestoreById_WhenIsDeletedEqualsTrue_ThenSoftRestored()
        {
            //given
            int sitterId = 4;

            //when
            _sut.RemoveOrRestoreById(sitterId);

            //then
            var actualSitter = _sut.GetById(sitterId);

            Assert.NotNull(actualSitter);
            Assert.False(actualSitter.IsDeleted);
        }

        [Test]
        public void Update_WhenCorrectDate_ThenChangeCharacteristics()
        {
            //given
            int sitterId = 1;

            var sitterForUpdate = new Sitter
            {
                Name = "Andrey",
                LastName = "Pistoletov",
                Phone = "89992226116",
                Age = 28,
                Experience = 7,
                Sex = Sex.Male,
                Description = "I love dogs"
            };

            //when
            _sut.Update(sitterForUpdate, sitterId);

            //then
            var actualSitter = _sut.GetById(sitterId);

            Assert.True(actualSitter.Name == sitterForUpdate.Name);
            Assert.True(actualSitter.LastName == sitterForUpdate.LastName);
            Assert.True(actualSitter.Phone == sitterForUpdate.Phone);
            Assert.True(actualSitter.Age == sitterForUpdate.Age);
            Assert.True(actualSitter.Experience == sitterForUpdate.Experience);
            Assert.True(actualSitter.Sex == sitterForUpdate.Sex);
            Assert.True(actualSitter.Description == sitterForUpdate.Description);
        }

        [Test]
        public void UpdatePassword_WhenCorrectDataPassed_ThenChangePassword()
        {
            //given
            string passwordForUpdate = "987654321";
            int sitterId = 2;

            //when
            _sut.UpdatePassword(sitterId, passwordForUpdate);

            //then
            var actualSitter = _sut.GetById(sitterId);

            Assert.True(actualSitter.Password == passwordForUpdate);
        }

        [Test]
        public void UpdatePriceCatalog_WhenCorrectDataPassed_ThenChangePriceCatalog()
        {
            //given
            var priceCatalogForUpdate = new List<PriceCatalog> 
            {
                new PriceCatalog
                {
                    //Service = new Service { Id = ServiceEnum.DailySitting },
                    Price = 500
                }
            };
            int sitterId = 3;

            //when
            _sut.UpdatePriceCatalog(sitterId, priceCatalogForUpdate);

            //then
            var actualSitter = _sut.GetById(sitterId);

            Assert.True(actualSitter.PricesCatalog[0].Price 
                == priceCatalogForUpdate[0].Price);
        }
    }
}
