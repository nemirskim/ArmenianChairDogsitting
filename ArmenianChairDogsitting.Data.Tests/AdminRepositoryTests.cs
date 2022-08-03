using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArmenianChairDogsitting.Data.Tests;

public class AdminRepositoryTests
{
    private DbContextOptions<ArmenianChairDogsittingContext> _dbContextOptions;
    private ArmenianChairDogsittingContext _context;
    private AdminRepository _sut;

    [SetUp]
    public void Setup()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ArmenianChairDogsittingContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb")
            .Options;

        _context = new ArmenianChairDogsittingContext(_dbContextOptions);
        _context.Database.EnsureDeleted();

        _sut = new AdminRepository(_context);

        _context.Admins.Add(new Admin
        {
            Password = "123456789",
            Email = "YaAdmin@mail.ru",
            IsDeleted = false
        });

        _context.Admins.Add(new Admin
        {
            Password = "987654321",
            Email = "IYaAdmin@mail.ru",
            IsDeleted = false
        });

        _context.Admins.Add(new Admin
        {
            Password = "1234509876",
            Email = "HtoYa@mail.ru",
            IsDeleted = true
        });
        _context.SaveChanges();
    }

    [Test]
    public void GetAdminById_WhenAdminIsExist_ThenReturnAdmin()
    {
        //given
        var expectedAdmin = new Admin
        {
            Id = 2,
            Password = "987654321",
            Email = "IYaAdmin@mail.ru",
            IsDeleted = false
        };

        int AdminId = 2;

        //when
        var actualAdmin = _sut.GetAdminById(AdminId);

        //then
        Assert.AreEqual(expectedAdmin.Id, actualAdmin.Id);
        Assert.AreEqual(expectedAdmin.Password, actualAdmin.Password);
        Assert.AreEqual(expectedAdmin.Email, actualAdmin.Email);
        Assert.False(actualAdmin.IsDeleted);
    }

    [Test]
    public void GetAdminByEmail_WhenAdminIsExist_ThenReturnAdmin()
    {
        //given
        var expectedAdmin = new Admin
        {
            Id = 1,
            Password = "123456789",
            Email = "YaAdmin@mail.ru",
            IsDeleted = false
        };

        //when
        var actualAdmin = _sut.GetAdminByEmail(expectedAdmin.Email);

        //then
        Assert.AreEqual(expectedAdmin.Id, actualAdmin.Id);
        Assert.AreEqual(expectedAdmin.Password, actualAdmin.Password);
        Assert.AreEqual(expectedAdmin.Email, actualAdmin.Email);
        Assert.False(actualAdmin.IsDeleted);
    }

    [Test]
    public void RemoveOrRestoreById_WhenAdminIsNotDeleted_ThenAdminDelete()
    {
        //given
        int adminId = 2;
        var admin = _sut.GetAdminById(adminId);
        admin.IsDeleted = true;

        //when
        _sut.RemoveOrRestoreById(admin);

        //then
        var actualSitter = _sut.GetAdminById(adminId);

        Assert.NotNull(actualSitter);
        Assert.True(actualSitter.IsDeleted);
    }

    [Test]
    public void RemoveOrRestoreById_WhenAdminIsDeleted_ThenAdminRestored()
    {
        //given
        int adminId = 3;
        var admin = _sut.GetAdminById(adminId);
        admin.IsDeleted = false;

        //when
        _sut.RemoveOrRestoreById(admin);

        //then
        var actualSitter = _sut.GetAdminById(adminId);

        Assert.NotNull(actualSitter);
        Assert.False(actualSitter.IsDeleted);
    }
}
