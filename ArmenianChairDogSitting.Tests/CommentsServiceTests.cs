using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Business.Services;
using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogSitting.Business.Tests;

public class CommentsServiceTests
{
    MockCommentsRepository _commentsRepository;
    CommentsService _commentsService;

    [SetUp]
    public void Setup()
    {
        _commentsRepository = new MockCommentsRepository();
        _commentsService = new CommentsService(_commentsRepository.Object);        
    }

    [Test]
    public void GetAllComments_WhenCommentsExist_ThenReturnMapedListOfComments()
    {
        //given
        var commentsInRepo = SetComments();
        _commentsRepository.MockGetAllComments(commentsInRepo);

        //when
        var actual = _commentsService.GetComments();

        //then
        Assert.IsTrue(actual is not null);
        Assert.IsTrue(actual!.Count > 0);
        Assert.IsTrue(actual is List<CommentModel>);
        //Assert.IsTrue(actual[0].Client is ClientModel)); later: need create moedel & maping to this one
        //Assert.IsTrue(actual[0].Order is OrderModel); later: need other branch
    }

    [Test]
    public void GetAllComments_WhenCommentsDoesntExist_ThenThrowNotFoundException()
    {
        //given
        var commentsInRepo = new List<Comment>();
        _commentsRepository.MockGetAllComments(commentsInRepo);

        //when then
        Assert.Throws<NotFoundException>(() => _commentsService.GetComments());
    }

    public void AddComment_WhenCalled_ThenReturnIdOfAddedComment()
    {
        //given
        var nowTime = DateTime.Now;
        var commentToAddModel = new CommentModel()
        {
            Id = 34,
            Title = "kwa kwa",
            TimeCreated = nowTime
        };

        var commentToAdd = new Comment()
        {
            Id = 34,
            Title = "kwa kwa",
            TimeCreated = nowTime
        };

        _commentsRepository.MockAddComment(commentToAdd);

        //when
        var returnedInt = _commentsService.AddComment(commentToAddModel);

        //then
        Assert.AreEqual(commentToAddModel.Id, returnedInt);
    }

    [Test]
    public void DeleteCommentById_WhenCommentExist_ThenKeepWorking()
    {
        //given
        var id = 2;
        _commentsRepository.MockGetById(id, new() { Id = id, IsDeleted = false});
        _commentsRepository.MockDeleteById(id);

        //when
        _commentsService.DeleteCommentById(id);

        //then
        Assert.Pass();
    }

    [Test]
    public void DeleteCommentById_WhenCommentDoesntExist_ThenThrowNotFoundException()
    {
        //given
        var id = 2;
        _commentsRepository.MockGetById(id, null);
        _commentsRepository.MockDeleteById(id);

        //when then
        Assert.Throws<NotFoundException>(() => _commentsService.DeleteCommentById(id));
    }

    private List<Comment> SetComments()
    {
        return new List<Comment>() {
            new() {
                Id = 1,
                Client = new() { Id = 1, Name = "Ivan"},
                IsDeleted = false,
                Order = new () {Id = 1},
                TimeCreated = DateTime.Now,
                Title = "blah blah blah"
            },
            new() {
                Id = 2,
                Client = new() { Id = 2, Name = "Georg"},
                IsDeleted = false,
                Order = new () {Id = 2},
                TimeCreated = DateTime.Now,
                Title = "blah blah blah"
            },
            new() {
                Id = 3,
                Client = new() { Id = 3, Name = "Lucius"},
                IsDeleted = false,
                Order = new () {Id = 3},
                TimeCreated = DateTime.Now,
                Title = "blah blah blah"
            }
        };
    }
}