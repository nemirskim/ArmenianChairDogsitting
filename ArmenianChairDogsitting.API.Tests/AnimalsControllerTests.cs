using ArmenianChairDogsitting.API.Controllers;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.API.Tests
{
    public class AnimalsControllerTests
    {
        private AnimalsController _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new AnimalsController();
        }

        [Test]
        public void AddAnimal_ValidRequestPassed_CreatedResultReceived()
        {
            var expectedId = 12;
            var dog = new DogRequest()
            {
                Name = "Bobik",
                Age = 12,
                RecommendationsForCare = "Gladit', lubit', gulyat'",
                ClientId = 1,
                Breed = "Korgi",
                Size = SizeOfAnimal.smaller_than_ten_kg
            };

            var actual = _sut.AddAnimal(dog);

            var actualResult = actual.Result as CreatedResult;

            Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
            Assert.AreEqual(expectedId, actualResult.Value);
        }

        [Test]
        public void GetAnimalById_ValidRequestPassed_OkReceived()
        {
            var expectedDog = new DogMainInfoResponse();
            var dogId = 1;

            var actual = _sut.GetAnimalById(dogId);

            var actualResult = actual.Result as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.AreEqual(expectedDog.GetType(), actualResult.Value.GetType());
        }

        [Test]
        public void GetAllAnimalsByClient_ValidRequestPassed_OkReceived()
        {
            var expectedListDogs = new List<DogAllInfoResponse>();
            var clientId = 1;

            var actual = _sut.GetAllAnimalsByClient(clientId);

            var actualResult = actual.Result as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.AreEqual(expectedListDogs.GetType(), actualResult.Value.GetType());
        }

        [Test]
        public void UpdateAnimalById_ValidRequestPassed_NoContentReceived()
        {
            var expectedDog = new DogUpdateRequest();
            var dogId = 3;

            var actual = _sut.UpdateAnimalById(expectedDog, dogId);

            var actualResult = actual as NoContentResult;

            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        }

        [Test]
        public void RemoveAnimalById_ValidRequestPassed_NoContentReceived()
        {
            var dogId = 2;

            var actual = _sut.RemoveAnimalById(dogId);

            var actualResult = actual as NoContentResult;

            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        }
    }
}
