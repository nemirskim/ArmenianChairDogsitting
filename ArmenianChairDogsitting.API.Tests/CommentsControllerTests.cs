using ArmenianChairDogsitting.API.Controllers;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ArmenianChairDogsitting.API.Tests
{
    public class CommentsControllerTests
    {
        private ÑommentsController _sut;
        private List<ValidationResult> _validationResult;
        private MockCommentsService _commentsService;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = APIMapperConfigStorage.GetInstance();
            _commentsService = new MockCommentsService();
            _sut = new ÑommentsController(_commentsService.Object, _mapper);
            _validationResult = new List<ValidationResult>();
        }

        [Test]
        public void AddComment_ValidRequestPassed_ThenCreatedResultReceived()
        {
            // given
            var expectedId = 1;
            var commentModel = new CommentModel() { Id = expectedId };

            var comment = new CommentRequest
            {
                ClientId = 1,
                OrderId = 2,
                Rating = 3,
                Text = "MinimanimanuMOO"
            };
            _commentsService.MockAddComment(commentModel);

            // when
            var actual = _sut.AddComment(comment);

            // then
            var actualResult = actual.Result as CreatedResult;

            var ctx = new ValidationContext(comment, null, null);
            Validator.TryValidateObject(comment, ctx, _validationResult, true);

            Assert.AreEqual(StatusCodes.Status201Created, actualResult!.StatusCode);
            Assert.AreEqual(expectedId, actualResult.Value);
            Assert.IsTrue(_validationResult.Any(
                v => v.MemberNames.Contains("ClientId") &&
                    v.MemberNames.Contains("OrderId") &&
                    v.MemberNames.Contains("Rating") &&
                    v.MemberNames.Contains("Text") &&
                    v.ErrorMessage!.Contains("required") &&
                    v.ErrorMessage.Contains("Range(0, 5)")));
        }

    }
}