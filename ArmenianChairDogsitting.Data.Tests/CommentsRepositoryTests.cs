using Microsoft.EntityFrameworkCore;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;

namespace ArmenianChairDogsitting.Data.Tests;

public class CommentsRepositoryTests
{
    private DbContextOptions<ArmenianChairDogsittingContext> _dbContextOptions;
    private ArmenianChairDogsittingContext _context;
    private CommentsRepository _sut;
    private DateTime _created;

    [SetUp]
    public void Setup()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ArmenianChairDogsittingContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb")
            .Options;

        _context = new ArmenianChairDogsittingContext(_dbContextOptions);
        _context.Database.EnsureDeleted();
        _sut = new CommentsRepository(_context);
        _created = DateTime.Now;

        _context.Comments.Add(new Comment()
        {
            IsDeleted = false,
            TimeCreated = _created,
            Text = "Chu papi mu nanyo",
            Client = new() { Name = "Grisha", LastName = "Grisha", Email = "ugabuga@kek.com", Password = " monkeySleep" },
            Order = new Order() { Address = "kwa wak swk 52" },
            Rating = 3,
        });

        _context.Comments.Add(new Comment()
        {
            IsDeleted = true,
            TimeCreated = _created,
            Text = "Chiki briki v damki",
            Client = new() { Name = "Egor", LastName = "Egor", Email = "ugabuga@kek.com", Password = " monkeySleep" },
            Order = new Order() { Address = "kwa wak swk 52" },
            Rating = 1
        });

        _context.Comments.Add(new Comment()
        {
            IsDeleted = false,
            TimeCreated = _created,
            Text = "Sitter Lost My Dog",
            Client = new() { Name = "Vova", LastName = "Vova", Email = "ugabuga@kek.com", Password = " monkeySleep" },
            Order = new Order() { Address = "kwa wak swk 52" },
            Rating = 5
        });

        _context.SaveChanges();
    }

    [Test]
    public void DeleteCommentById_WhenCorrectIdPassed_ThenDeleteComment()
    {
        //given 
        var id = 1;
        var comment = _context.Comments.FirstOrDefault(c => c.Id == id);

        //when
        _sut.DeleteCommentById(id);

        //then
        Assert.IsTrue(comment!.IsDeleted);
        Assert.NotNull(comment.TimeUpdated);
        Assert.AreEqual(_created, comment.TimeCreated);
        Assert.IsTrue(comment.TimeUpdated > comment.TimeCreated);
    }
}
