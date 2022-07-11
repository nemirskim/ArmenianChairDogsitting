using ArmenianChairDogsitting.API.Controllers;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Moq;
using ArmenianChairDogsitting.Business.Services;
using ArmenianChairDogsitting.Business.Interfaces;

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
                ClientId = 1,
                OrderId = 2,
                Rating = 3,
                Text = "MinimanimanuMOO"
            };

            var expectedCommentModel = new CommentModel
            {
                Client = new() { Id = comment.ClientId },
                Order = new() { Id = comment.OrderId },
                Rating = comment.Rating,
                Text = comment.Text,
                IsDeleted = false,
                TimeCreated = DateTime.MinValue
            };

            _commentsServiceMock
                .Setup(x => x.AddComment(It.IsAny<CommentModel>()))
                .Returns(expectedId);

            // when
            var actual = _sut.AddComment(comment);

            // then
            var actualResult = actual.Result as CreatedResult;

            //var ctx = new ValidationContext(comment, null, null);
            //Validator.TryValidateObject(comment, ctx, _validationResult, true);

            Assert.AreEqual(StatusCodes.Status201Created, actualResult!.StatusCode);
            Assert.AreEqual(expectedId, actualResult.Value);
            //Assert.IsTrue(_validationResult.Any(
            //    v => v.MemberNames.Contains("ClientId") &&
            //        v.MemberNames.Contains("OrderId") &&
            //        v.MemberNames.Contains("Rating") &&
            //        v.MemberNames.Contains("Text") &&
            //        v.ErrorMessage!.Contains("required") &&
            //        v.ErrorMessage.Contains("Range(0, 5)")));

            _commentsServiceMock.Verify(x => x.AddComment(It.Is<CommentModel>(c => 
                c.IsDeleted == expectedCommentModel.IsDeleted &&
                c.Rating == expectedCommentModel.Rating &&
                c.Text == expectedCommentModel.Text &&
                c.TimeCreated == expectedCommentModel.TimeCreated &&
                c.Order.Id == expectedCommentModel.Order.Id &&
                c.Client.Id == expectedCommentModel.Client.Id
            )), Times.Once);
        }

    }
}