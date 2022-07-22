using ArmenianChairDogsitting.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Moq;
using ArmenianChairDogsitting.Business.Interfaces;

namespace ArmenianChairDogsitting.API.Tests
{
    public class CommentsControllerTests
    {
        private ÑommentsController _sut;
        private Mock<ICommentsService> _commentsServiceMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new APIMapperConfigStorage());
            });
            _mapper = mockMapper.CreateMapper();
            _commentsServiceMock = new Mock<ICommentsService>();
            _sut = new ÑommentsController(_commentsServiceMock.Object, _mapper);
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