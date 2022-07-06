using NUnit.Framework;
using ArmenianChairDogsitting.API;
using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.API.Controllers;
using Microsoft.AspNetCore.Http;
using ArmenianChairDogsitting.API.Models;
using System.ComponentModel.DataAnnotations;
using ArmenianChairDogsitting.API.Enums;
using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.API.Tests
{
    [ApiController]
    [Route("[controller]")]
    public class OrderTests
    {
        private OrdersController _sut;
        private List<ValidationResult> _validationResult;

        [SetUp]
        public void Setup()
        {
            _sut = new OrdersController(null);
            _validationResult = new List<ValidationResult>();
        }

        [Test]
        public void AddOrder_ValidRequestPassed_CreatedResultReceived()
        {
            // given
            var expectedId = 1;
            var order = new Order();
            
            // when
            var actual = _sut.AddOrder(order);

            // then
            var actualResult = actual.Result as CreatedResult;

            var ctx = new ValidationContext(order, null, null);
            Validator.TryValidateObject(order, ctx, _validationResult, true);

            Assert.AreEqual(StatusCodes.Status201Created, actualResult!.StatusCode);
            Assert.AreEqual(expectedId, actualResult.Value);
            Assert.IsTrue(_validationResult.Any(
                v => v.MemberNames.Contains("WalkQuantity") &&
                    v.MemberNames.Contains("ClientId") &&
                    v.MemberNames.Contains("Animals") &&
                    v.MemberNames.Contains("Status") &&
                    v.MemberNames.Contains("IsTrial") &&
                    v.ErrorMessage!.Contains("required") &&
                    v.ErrorMessage.Contains("MinLength(1)") &&
                    v.ErrorMessage.Contains("MaxLength(4)") &&
                    v.ErrorMessage.Contains("Range(1, 60)")));
        }

        [Test]
        public void AddOrder_InvalidPropertysRequest_ShouldThrowUnprocessableEntity()
        {
            // given
            var expectedId = 1;
            var order = new Order();

            // when
            var actual = _sut.AddOrder(order);

            // then
            var actualResult = actual.Result as CreatedResult;

            var ctx = new ValidationContext(order, null, null);
            Validator.TryValidateObject(order, ctx, _validationResult, true);

            Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, actualResult!.StatusCode);
            Assert.AreEqual(expectedId, actualResult.Value);
            Assert.IsTrue(_validationResult.Any(
                v => v.MemberNames.Contains("WalkQuantity") &&
                    v.MemberNames.Contains("ClientId") &&
                    v.MemberNames.Contains("Animals") &&
                    v.MemberNames.Contains("Status") &&
                    v.MemberNames.Contains("IsTrial") &&
                    v.ErrorMessage!.Contains("required") &&
                    v.ErrorMessage.Contains("MinLength(1)") &&
                    v.ErrorMessage.Contains("MaxLength(4)") &&
                    v.ErrorMessage.Contains("Range(1, 60)")));
        }

        [Test]
        public void ChangeOrderStatus_ValidRequest_ShouldThrowNoContent()
        {
            //given
            var orderId = 2;
            var status = Status.Created;

            //when
            var actual = _sut.ChangeOrderStatus(status, orderId);

            //then
            var actualResult = actual as NoContentResult;

            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult!.StatusCode);
        }

        [Test]
        public void ChangeOrderStatus_InvalidRequest_ShouldThrowUnprocessableEntity()
        {
            //given
            var orderId = -1;
            var status = Status.Created;

            //when
            var actual = _sut.ChangeOrderStatus(status, orderId);

            //then
            var actualResult = actual as UnprocessableEntityResult;

            Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, actualResult!.StatusCode);
        }

        [Test]
        public void GetOrderById_ValidRequest_ShouldThrowOk()
        {
            //given
            var orderId = 1;
            var expectedResponse = new OrderWalkResponse() { Id = orderId} ;

            //when
            var actual = _sut.GetOrderById(orderId);

            //then
            var actualResult = actual.Result as OkObjectResult;
            var actulResponse = actualResult.Value as OrderWalkResponse;

            Assert.AreEqual(StatusCodes.Status200OK, actualResult!.StatusCode);
            Assert.AreEqual(expectedResponse.Id, actulResponse.Id);
        }

        [Test]
        public void GetOrderById_WhenIdDoesntExist_ShouldThrowNotFound()
        {
            //given
            var orderId = -1;
            OrderWalkResponse expectedResponse = null;

            //when 
            var actual = _sut.GetOrderById(orderId);

            //then
            var actualResult = actual.Result as NotFoundResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, actualResult!.StatusCode);
        }

        [Test]
        public void GetAllOrders_WhenResponseIsNotNull_ShouldThrowOk()
        {
            //given
            var expectedResponseObject = new List<AbstractOrderResponse>() { new OrderWalkResponse()};

            //when
            var actual = _sut.GetAllOrders();

            //then
            var actualResult = actual.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        }

        [Test]
        public void GetAllOrders_WhenResponseIsEmpty_ShouldThrowNotFound()
        {
            //given
            var expectedResponseObject = new List<AbstractOrderResponse>();

            //when
            var actual = _sut.GetAllOrders();

            //then
            var actualResult = actual.Result as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, actualResult.StatusCode);
            Assert.AreEqual(expectedResponseObject.Count, actual.Value.Count);
        }
    }
}