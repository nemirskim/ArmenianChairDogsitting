using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Services;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;
using Moq;

namespace ArmenianChairDogSitting.Business.Tests;

public class CommentsServiceTests
{
    private Mock<ICommentsRepository> _commentsRepository;
    private CommentsService _sut;

    [SetUp]
    public void Setup()
    {
        _commentsRepository = new Mock<ICommentsRepository>();
        _sut = new CommentsService(_commentsRepository.Object);        
    }

    [Test]
    public void DeleteCommentById_WhenCommentExist_ThenKeepWorking()
    {
        //given
        var id = 2;

        _commentsRepository
            .Setup(x => x.GetCommentById(id))
            .Returns(new Comment() { Id = id, IsDeleted = false });

        //when
        _sut.DeleteCommentById(id);

        //then
        _commentsRepository.Verify(x => x.GetCommentById(id), Times.Once);
        _commentsRepository.Verify(x => x.DeleteCommentById(id), Times.Once);
    }

    [Test]
    public void DeleteCommentById_WhenCommentDoesntExist_ThenThrowNotFoundException()
    {
        //given
        var id = 2;

        Comment comment = null;

        _commentsRepository
            .Setup(x => x.GetCommentById(id))
            .Returns(comment);

        //when then
        Assert.Throws<NotFoundException>(() => _sut.DeleteCommentById(id));
        _commentsRepository.Verify(x => x.GetCommentById(id), Times.Once);
        _commentsRepository.Verify(x => x.DeleteCommentById(id), Times.Never);
    }
}