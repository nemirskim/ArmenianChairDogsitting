using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Services;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;
using Moq;

namespace ArmenianChairDogsitting.Business.Tests;

public class AdminServiceTests
{
    private Mock<IAdminRepository> _adminRepository;
    private AdminService _sut;

    [SetUp]
    public void Setup()
    {
        _adminRepository = new Mock<IAdminRepository>();
        _sut = new AdminService(_adminRepository.Object);
    }

    [Test]
    public void AddAdmin_WhenValidationPassed_ThenReturnIdOfAddedAdmin()
    {
        //given
        var expectedId = 1;

        var admin = new Admin()
        { 
            Email = "pistol@pi.com",
            Password = "123456789",
            IsDeleted = false
        };

        _adminRepository.Setup(c => c.Add(admin))
             .Returns(expectedId);

        //when
        var actual = _sut.AddAdmin(admin);


        //then

        Assert.AreEqual(actual, expectedId);
        _adminRepository.Verify(c => c.Add(admin), Times.Once);
    }

    [Test]
    public void RemoveOrRestoreById_WhenAdminIsNotDeleted_ThenDeleteAdmin()
    {
        ///given
        var expectedAdmin = new Admin()
        {
            Id = 1,
            Email = "pistol@pi.com",
            Password = "123456789",
            IsDeleted = false
        };

        _adminRepository.Setup(o => o.GetAdminById(expectedAdmin.Id)).Returns(expectedAdmin);

        //when
        _sut.RemoveOrRestoreById(expectedAdmin.Id, true);


        //then
        var actualAdmin = _sut.GetAdminById(expectedAdmin.Id);

        Assert.True(actualAdmin.IsDeleted);

        _adminRepository.Verify(c => c.RemoveOrRestoreById(expectedAdmin), Times.Once);
        _adminRepository.Verify(c => c.GetAdminById(expectedAdmin.Id), Times.Exactly(2));
    }

    [Test]
    public void RemoveOrRestoreById_WhenAdminIsDeleted_ThenRestoreAdmin()
    {
        ///given
        var expectedAdmin = new Admin()
        {
            Id = 10,
            Email = "pistol@pi.com",
            Password = "123456789",
            IsDeleted = true
        };

        _adminRepository.Setup(o => o.GetAdminById(expectedAdmin.Id)).Returns(expectedAdmin);

        //when
        _sut.RemoveOrRestoreById(expectedAdmin.Id, false);

        //then
        var actualAdmin = _sut.GetAdminById(10);

        Assert.False(actualAdmin.IsDeleted);

        _adminRepository.Verify(c => c.RemoveOrRestoreById(expectedAdmin), Times.Once);
        _adminRepository.Verify(c => c.GetAdminById(expectedAdmin.Id), Times.Exactly(2));
    }

    [Test]
    public void RemoveOrRestoreById_WhenAdminIsNotExist_ThenNotFoundExeption()
    {
        //given
        int adminId = 1;

        var admin = new Admin()
        {
            Id = 10,
            Email = "pistol@pi.com",
            Password = "123456789",
            IsDeleted = false
        };

        _adminRepository.Setup(o => o.GetAdminById(admin.Id)).Returns(admin);

        //then, when
        Assert.Throws<NotFoundException>(() => _sut.RemoveOrRestoreById(adminId, true));
    }

    [Test]
    public void GetAdminById_WhenAdminExist_ThenReturnAdmin()
    {
        //given
        var expectedAdmin = new Admin()
        {
            Id = 1,
            Email = "pistol@pi.com",
            Password = "123456789",
            IsDeleted = false
        };

        _adminRepository
            .Setup(x => x.GetAdminById(expectedAdmin.Id))
            .Returns(expectedAdmin);

        //when
        var actualAdmin = _sut.GetAdminById(expectedAdmin.Id);

        //then
        Assert.AreEqual(actualAdmin.Id, expectedAdmin.Id);
        Assert.AreEqual(actualAdmin.Password, expectedAdmin.Password);
        Assert.AreEqual(actualAdmin.Email, expectedAdmin.Email);
        Assert.False(actualAdmin.IsDeleted);

        _adminRepository.Verify(x => x.GetAdminById(expectedAdmin.Id), Times.Once);
    }
}
