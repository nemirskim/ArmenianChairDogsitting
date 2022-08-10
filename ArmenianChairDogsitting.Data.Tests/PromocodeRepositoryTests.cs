using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArmenianChairDogsitting.Data.Tests
{
    public class PromocodeRepositoryTests
    {
        private DbContextOptions<ArmenianChairDogsittingContext> _dbContextOptions;
        private ArmenianChairDogsittingContext _context;
        private PromocodeRepository _sut;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ArmenianChairDogsittingContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb")
            .Options;

            _context = new ArmenianChairDogsittingContext(_dbContextOptions);
            _context.Database.EnsureDeleted();
            _sut = new PromocodeRepository(_context);

            _context.Promocodes.Add(new Promocode()
            {
                Discount = (decimal)(0.5),
                EndDate = DateTime.MinValue,
                StartDate = DateTime.MinValue,
                Promo = "Test"
            });
            _context.SaveChanges();

            var date = new DateTime(2022, 8, 10);

            _context.Promocodes.Add(new Promocode()
            {
                Discount = (decimal)(0.5),
                EndDate = date.AddDays(2),
                StartDate = date.AddDays(-1),
                Promo = "TestPromo"
            });
            _context.SaveChanges();
        }

        [Test]
        public void GetPromocode_WhenPromocodeFound_ReturnPromocode()
        {
            //given
            var date = new DateTime(2022, 8, 10);
            var expectedPromocode = new Promocode()
            {
                Discount = (decimal)(0.5),
                EndDate = date.AddDays(2),
                StartDate = date.AddDays(-1),
                Promo = "TestPromo"
            };
            var promocode = "TestPromo";

            //when
            var returnedPromocoded = _sut.GetPromocode(promocode);

            //then
            Assert.AreEqual(returnedPromocoded.Discount, returnedPromocoded.Discount);
            Assert.AreEqual(returnedPromocoded.EndDate, returnedPromocoded.EndDate);
            Assert.AreEqual(returnedPromocoded.StartDate, returnedPromocoded.StartDate);
            Assert.AreEqual(returnedPromocoded.Promo, returnedPromocoded.Promo);
        }

        [Test]
        public void GetPromocode_WhenPromocodeNotFound_ReturnNull()
        {
            //given
            Promocode expectedPromocode = null;
            var promocode = "TestNegativePromo";

            //when
            var returnedPromocoded = _sut.GetPromocode(promocode);

            //then
            Assert.AreEqual(expectedPromocode, returnedPromocoded);
        }
    }
}