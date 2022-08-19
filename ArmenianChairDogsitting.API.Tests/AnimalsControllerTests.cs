using ArmenianChairDogsitting.API.Controllers;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AutoMapper;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.API.Tests
{
    public class AnimalsControllerTests
    {
        private AnimalsController _sut;
        private Mock<IAnimalsService> _animalsServiceMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new APIMapperConfigStorage());
            });
            _mapper = mockMapper.CreateMapper();
            _animalsServiceMock = new Mock<IAnimalsService>();
            _sut = new AnimalsController(_animalsServiceMock.Object, _mapper);
        }

        [Test]
        public void AddAnimal_ValidRequestPassed_CreatedResultReceived()
        {
            //given
            var expectedId = 12;
            var dog = new DogRequest()
            {
                Name = "Bobik",
                Age = 12,
                RecommendationsForCare = "Gladit', lubit', gulyat'",
                ClientId = 1,
                Breed = "Korgi",
                Size = SizeOfAnimal.SmallerThanTenKg
            };

            _animalsServiceMock
                .Setup(a => a.AddAnimal(It.IsAny<Animal>())).Returns(expectedId);

            //when
            var actual = _sut.AddAnimal(dog);

            //then
            var actualResult = actual.Result as CreatedResult;

            Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
            Assert.AreEqual(expectedId, actualResult.Value);

            _animalsServiceMock.Verify(a => a.AddAnimal(It.Is<Animal>(a =>
                a.Name == dog.Name &&
                a.Age == dog.Age &&
                a.RecommendationsForCare == dog.RecommendationsForCare &&
                a.ClientId == dog.ClientId &&
                a.Breed == dog.Breed &&
                a.Size == dog.Size
            )), Times.Once);
        }

        [Test]
        public void GetAnimalById_ValidRequestPassed_ReturnExpectedAnimal()
        {
            //given
            var dog = new Animal()
            {
                Id = 1,
                Name = "Warik",
                Breed = "Ovcharka",
                Age = 4,
                Size = SizeOfAnimal.SmallerThanTenKg,
                RecommendationsForCare = "gladit'",
                ClientId = 2,
                Orders = new List<Order>()
            };

            _animalsServiceMock
                .Setup(a => a.GetAnimalById(dog.Id)).Returns(dog);

            //when
            var actual = _sut.GetAnimalById(dog.Id);

            //then
            var actualResult = actual.Result as ObjectResult;
            var dogMainInfoResponse = actualResult.Value as DogMainInfoResponse;

            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.Multiple(() =>
            {
                Assert.That(dogMainInfoResponse.Id, Is.EqualTo(dog.Id));
                Assert.That(dogMainInfoResponse.Name, Is.EqualTo(dog.Name));
                Assert.That(dogMainInfoResponse.Breed, Is.EqualTo(dog.Breed));
                Assert.That(dogMainInfoResponse.Age, Is.EqualTo(dog.Age));
                Assert.That(dogMainInfoResponse.Size, Is.EqualTo(dog.Size));
                Assert.That(dogMainInfoResponse.RecommendationsForCare, Is.EqualTo(dog.RecommendationsForCare));
            });

            _animalsServiceMock.Verify(a => a.GetAnimalById(dog.Id), Times.Once);
        }

        [Test]
        public void GetAllAnimalsByClient_ValidRequestPassed_OkReceived()
        {
            //given
            var dogs = new List<Animal>
        {
            new Animal()
            {
                Id = 3,
                Name = "Bublik",
                Breed = "wpic",
                Age = 5,
                Size = SizeOfAnimal.SmallerThanFiveKg,
                RecommendationsForCare = "celovat'",
                ClientId = 2
            },

            new Animal()
            {
                Id = 5,
                Name = "Popa",
                Breed = "ovcharka",
                Age = 3,
                Size = SizeOfAnimal.SmallerThanTenKg,
                RecommendationsForCare = "lubit'",
                ClientId = 2
            }
        };

            _animalsServiceMock
                .Setup(a => a.GetAllAnimalsByClient(2)).Returns(dogs);

            //when
            var actual = _sut.GetAllAnimalsByClient(2);

            //then
            var actualResult = actual.Result as ObjectResult;
            var dogsAllInfoResponse = actualResult.Value as List<DogAllInfoResponse>;

            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);

            Assert.Multiple(() =>
            {
                Assert.That(dogsAllInfoResponse.Count, Is.EqualTo(dogs.Count));
                Assert.That(dogsAllInfoResponse[0].Name, Is.EqualTo(dogs[0].Name));
                Assert.That(dogsAllInfoResponse[1].Name, Is.EqualTo(dogs[1].Name));
                Assert.That(dogsAllInfoResponse[1].Breed, Is.EqualTo(dogs[1].Breed));
                Assert.That(dogsAllInfoResponse[0].Breed, Is.EqualTo(dogs[0].Breed));
            });

            _animalsServiceMock.Verify(a => a.GetAllAnimalsByClient(2), Times.Once);
        }

        [Test]
        public void UpdateAnimalById_ValidRequestPassed_NoContentReceived()
        {
            //given
            var dogId = 3;
            var dogToUpdate = new DogUpdateRequest()
            {
                Name = "Max",
                Age = 5,
                Breed = "Korgi",
                RecommendationsForCare = "Gylyat'",
                Size = SizeOfAnimal.SmallerThanTenKg
            };

            _animalsServiceMock
                .Setup(a => a.UpdateAnimal(It.IsAny<Animal>(), dogId));

            //when
            var actual = _sut.UpdateAnimalById(dogToUpdate, dogId);

            //then
            var actualResult = actual as NoContentResult;

            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

            _animalsServiceMock.Verify(a => a.UpdateAnimal(It.Is<Animal>(a => 
            a.Name == dogToUpdate.Name &&
            a.Age == dogToUpdate.Age &&
            a.Breed == dogToUpdate.Breed &&
            a.RecommendationsForCare == dogToUpdate.RecommendationsForCare &&
            a.Size == dogToUpdate.Size
            ), It.Is<int>(i => i == dogId)), Times.Once);
        }

        [Test]
        public void RemoveAnimalById_ValidRequestPassed_NoContentReceived()
        {
            //given
            var dogId = 2;

            //when
            var actual = _sut.RemoveAnimal(dogId);

            //then
            var actualResult = actual as NoContentResult;

            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

            _animalsServiceMock.Verify(a => a.RemoveOrRestoreAnimal(dogId, true), Times.Once);
        }

        [Test]
        public void RestoreAnimalById_ValidRequestPassed_NoContentReceived()
        {
            //given
            var dogId = 2;

            //when
            var actual = _sut.RestoreAnimal(dogId);

            //then
            var actualResult = actual as NoContentResult;

            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

            _animalsServiceMock.Verify(a => a.RemoveOrRestoreAnimal(dogId, false), Times.Once);
        }
    }
}
