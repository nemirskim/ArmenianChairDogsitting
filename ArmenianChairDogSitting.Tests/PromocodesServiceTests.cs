using ArmenianChairDogsitting.Business.Services;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;
using Moq;

namespace ArmenianChairDogsitting.Business.Tests;

public class PromocodesServiceTests
{
    private Mock<IPromocodeRepository> _promocodesRepository;
    private PromocodesService _sut;

    [SetUp]
    public void Setup()
    {
        _promocodesRepository = new Mock<IPromocodeRepository>();
        _sut = new PromocodesService(_promocodesRepository.Object);
    }

    [Test]
    public void GetDiscount_WhenAllConditionIsFine_ReturnDiscount()
    {
        //given 
        var promocode = "TestPromo";
        var expectedDiscount = (decimal)0.7;
        var date = DateTime.Now;

        var returningPromocodeEntity = new Promocode()
        {
            Discount = (decimal)(0.7),
            EndDate = date.AddDays(2),
            StartDate = date.AddDays(-1),
            Promo = "TestPromo"
        };

        _promocodesRepository
            .Setup(x => x.GetPromocode(promocode))
            .Returns(returningPromocodeEntity);

        //when 
        var actualDiscount = _sut.GetDiscount(promocode);

        //then
        Assert.AreEqual(expectedDiscount, actualDiscount);

        _promocodesRepository.Verify(x => x.GetPromocode(promocode), Times.Once);
    }

    [Test]
    public void GetDiscount_WhenPromocodeNotFoundd_ReturnDeafaultDiscount()
    {
        //given 
        var promocode = "TestPromo";
        var expectedDiscount = (decimal)1;
        var date = new DateTime(2022, 8, 10);

        Promocode returningPromocodeEntity = null;

        _promocodesRepository
            .Setup(x => x.GetPromocode(promocode))
            .Returns(returningPromocodeEntity);

        //when 
        var actualDiscount = _sut.GetDiscount(promocode);

        //then
        Assert.AreEqual(expectedDiscount, actualDiscount);

        _promocodesRepository.Verify(x => x.GetPromocode(promocode), Times.Once);
    }

    [Test]
    public void GetDiscount_WhenDateIsOutOfRange_ReturnDeafaultDiscount()
    {
        //given 
        var promocode = "TestPromo";
        var expectedDiscount = (decimal)1;
        var date = new DateTime(2022, 8, 10);

        var returningPromocodeEntity = new Promocode()
        {
            Discount = (decimal)(0.7),
            EndDate = date.AddDays(-5),
            StartDate = date.AddDays(-10),
            Promo = "TestPromo"
        };

        _promocodesRepository
            .Setup(x => x.GetPromocode(promocode))
            .Returns(returningPromocodeEntity);

        //when 
        var actualDiscount = _sut.GetDiscount(promocode);

        //then
        Assert.AreEqual(expectedDiscount, actualDiscount);

        _promocodesRepository.Verify(x => x.GetPromocode(promocode), Times.Once);
    }
}
