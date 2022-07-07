using Microsoft.EntityFrameworkCore;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;

namespace ArmenianChairDogsitting.Data.Tests;

public class CommentsRepositoryTests
{
    private DbContextOptions<ArmenianChairDogsittingContext> _dbContextOptions;
    private ArmenianChairDogsittingContext _context;
    private CommentsRepository _sut;

    [SetUp]
    public void Setup()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ArmenianChairDogsittingContext>()
        .UseInMemoryDatabase(databaseName: "TestDb")
        .Options;

        _context = new ArmenianChairDogsittingContext(_dbContextOptions);
        _sut = new CommentsRepository(_context);

        _context.Comments.Add(new Comment()
        {
            Id = 1,
            IsDeleted = false,
            TimeCreated = DateTime.Now,
            Title = "Chu papi mu nanyo",
            Client = new() { Id = 1 },
            Order = new OrderOverexpose() { Id = 1 },
            Rating = 3
        }) ;

        _context.Comments.Add(new Comment()
        {
            Id = 2,
            IsDeleted = true,
            TimeCreated = DateTime.Now,
            Title = "Chiki briki v damki",
            Client = new() { Id = 2 },
            Order = new OrderOverexpose() { Id = 2 },
            Rating = 1
        });

        _context.Comments.Add(new Comment()
        {
            Id = 3,
            IsDeleted = false,
            TimeCreated = DateTime.Now,
            Title = "Sitter Lost My Dog",
            Client = new() { Id = 1 },
            Order = new OrderWalk() { Id = 3 },
            Rating = 5
        });

        _context.SaveChanges();
    }

    [Test]
    public void DeleteCommentById_WhenCorrectIdPassed_ThenDeleteComment()
    {
        //given 
        var now = DateTime.Now;
        var id = 1;
        var comment = _context.Comments.FirstOrDefault(c => c.Id == id);

        //when
        _sut.DeleteCommentById(id);

        //then
        Assert.IsTrue(comment!.IsDeleted);
        Assert.NotNull(comment.TimeUpdated);
        Assert.AreEqual(now, comment.TimeCreated);
        Assert.IsTrue(comment.TimeUpdated > comment.TimeCreated);
    }

    [Test]
    public void GetAllComments_WhenCalled_ReturnsAllComments()
    {
        //given
        var expectedQuantityIttems = 2;

        //when
        var comments = _sut.GetAllComments();

        //then
        Assert.AreEqual(expectedQuantityIttems, comments.Count);
    }
}
