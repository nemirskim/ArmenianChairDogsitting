using ArmenianChairDogsitting.API.Controllers;
using ArmenianChairDogsitting.API.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Moq;
using ArmenianChairDogsitting.Business.Services;
using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.API.Tests
{
    public class CommentsControllerTests
    {
        private ÑommentsController _sut;
        private List<ValidationResult> _validationResult;
        private Mock<ICommentsService> _commentsServiceMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = APIMapperConfigStorage.GetInstance();
            _commentsServiceMock = new Mock<ICommentsService>();
            _sut = new ÑommentsController(_commentsServiceMock.Object, _mapper);
            _validationResult = new List<ValidationResult>();
        }

        [Test]
        public void AddComment_ValidRequestPassed_ThenCreatedResultReceived()
        {
            // given
            var expectedId = 1;

            var comment = new CommentRequest
            {
                Rating = 3,
                Text = "MinimanimanuMOO"
            };

            var expectedCommentModel = new Comment
            {
                Rating = comment.Rating,
                Text = comment.Text,
                IsDeleted = false,
                TimeCreated = DateTime.MinValue
            };

            _commentsServiceMock
                .Setup(x => x.AddComment(It.IsAny<Comment>()))
                .Returns(expectedId);

            // when
            var actual = _sut.AddComment(comment);

            // then
            var actualResult = actual.Result as CreatedResult;

            Assert.AreEqual(StatusCodes.Status201Created, actualResult!.StatusCode);
            Assert.AreEqual(expectedId, actualResult.Value);

            _commentsServiceMock.Verify(x => x.AddComment(It.Is<Comment>(c => 
                c.IsDeleted == expectedCommentModel.IsDeleted &&
                c.Rating == expectedCommentModel.Rating &&
                c.Text == expectedCommentModel.Text &&
                c.TimeCreated == expectedCommentModel.TimeCreated
            )), Times.Once);
        }

        

        [Test]
        public void GetAllComments_WhenValidRequestPassed_ThenReturnListOfComments()
        {
            // given
            var derivedComments = new List<Comment>();

            var expectedComments = new List<CommentResponse>();

            _commentsServiceMock
                .Setup(x => x.GetComments())
                .Returns(derivedComments);

            //when 
            var comments = _sut.GetAllComments();

            //then
            var resultComments = comments.Result as OkObjectResult;
            var actualComments = resultComments.Value as List<CommentResponse>;

            Assert.AreEqual(derivedComments.Count, actualComments.Count);
            Assert.AreEqual(expectedComments.Count, actualComments.Count);
            Assert.AreEqual(expectedComments.GetType(), actualComments.GetType());
            Assert.AreEqual(StatusCodes.Status200OK, resultComments.StatusCode);
            _commentsServiceMock.Verify(x => x.GetComments(), Times.Once);
        }

        [Test]
        public void DeleteCommentById_WhenValidRequestPassed_ThenNoContent()
        {
            //given
            var id = 2;

            _commentsServiceMock
                .Setup(x => x.DeleteCommentById(It.IsAny<int>()));

            //when
            var result = _sut.DeleteCommentById(id) as NoContentResult;

            //then
            Assert.AreEqual(StatusCodes.Status204NoContent, result.StatusCode);
            _commentsServiceMock.Verify(x => x.DeleteCommentById(It.IsAny<int>()), Times.Once);

        }

    }
}