using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Hashing;
using ArmenianChairDogsitting.Business.Services;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;
using Moq;

namespace ArmenianChairDogsitting.Business.Tests
{
    public class AuthServiceTests
    {
        private AuthService _sut;
        private Mock<IClientsRepository> _clientsRepositoryMock;
        private Mock<ISitterRepository> _sittersRepository;
        private Mock<IAdminRepository> _adminRepository;

        public void Setup()
        {

            _clientsRepositoryMock = new Mock<IClientsRepository>();
            _sittersRepository = new Mock<ISitterRepository>();
            _adminRepository = new Mock<IAdminRepository>();
            _sut = new AuthService(_adminRepository.Object, _sittersRepository.Object, _clientsRepositoryMock.Object);
        }

        [Test]
        public void Login_ValidAdminEmailAndPassword_ThenClaimModelReturned()
        {
            Setup();
            //given
            var adminPassword = "12345678954";
            var adminExpected = new Admin()
            {
                Id = 3,
                Password = PasswordHash.HashPassword("12345678954"),
                Email = "Wyaadmin@gmail.com",
                IsDeleted = false,
            };

            _adminRepository.Setup(m => m.GetAdminByEmail(adminExpected.Email)).Returns(adminExpected);
           
            //when
            var claim = _sut.GetUserForLogin(adminExpected.Email, adminPassword);
            
            //then
            Assert.True(claim.Role == Role.Admin.ToString());
            Assert.True(claim.Email == adminExpected.Email);
            _adminRepository.Verify(c => c.GetAdminByEmail(It.IsAny<string>()), Times.Once);
            _sittersRepository.Verify(c => c.GetSitterByEmail(It.IsAny<string>()), Times.Never);
            _clientsRepositoryMock.Verify(c => c.GetClientByEmail(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Login_ValidClientEmailAndPassword_ThenClaimModelReturned()
        {
            Setup();
            //given
            var clientPassword = "1234567890";
            var clientExpected = new Client()
            {
                Name = "Timur",
                LastName = "Timyrov",
                Password = PasswordHash.HashPassword(clientPassword),
                Email = "Timyr@gmail.com",
            };

            _clientsRepositoryMock.Setup(c => c.GetClientByEmail(clientExpected.Email)).Returns(clientExpected);

            //when
            var claim = _sut.GetUserForLogin(clientExpected.Email, clientPassword);

            //then
            Assert.True(claim.Role == Role.Client.ToString());
            Assert.True(claim.Email == clientExpected.Email);
            _clientsRepositoryMock.Verify(c => c.GetClientByEmail(It.IsAny<string>()), Times.Once);
            _adminRepository.Verify(c => c.GetAdminByEmail(It.IsAny<string>()), Times.Once);
            _sittersRepository.Verify(c => c.GetSitterByEmail(It.IsAny<string>()), Times.Once);

        }

        [Test]
        public void Login_ValidCleanersEmailAndPassword_ThenClaimModelReturned()
        {
            Setup();
            //given
            var passwordCleanersExpected = "0987654321";
            var cleanersExpected = new Sitter()
            {
                Name = "Helen",
                LastName = "Nehelen",
                Email = "iLoveDoggy@gmail.com",
                Password = PasswordHash.HashPassword(passwordCleanersExpected)
            };

            _sittersRepository.Setup(c => c.GetSitterByEmail(cleanersExpected.Email)).Returns(cleanersExpected);

            //when
            var claim = _sut.GetUserForLogin(cleanersExpected.Email, passwordCleanersExpected);

            //then
            Assert.True(claim.Role == Role.Sitter.ToString());
            Assert.True(claim.Email == cleanersExpected.Email);
            _clientsRepositoryMock.Verify(c => c.GetClientByEmail(It.IsAny<string>()), Times.Once);
            _adminRepository.Verify(c => c.GetAdminByEmail(It.IsAny<string>()), Times.Once);
            _sittersRepository.Verify(c => c.GetSitterByEmail(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Login_BadPassword_ThenThrowNotFoundException()
        {
            Setup();
            //given
            var badPassword = "135792468";
            var cleanersExpected = new Sitter()
            {
                Name = "Helen",
                LastName = "Nehelen",
                Email = "iLoveDoggy@gmail.com",
                Password = PasswordHash.HashPassword("111222333"),
            };

            _sittersRepository.Setup(c => c.GetSitterByEmail(cleanersExpected.Email)).Returns(cleanersExpected);

            //when, then
            Assert.Throws<NotFoundException>(() => _sut.GetUserForLogin(cleanersExpected.Email, badPassword));

        }

        [Test]
        public void Login_IsDeletedTrue_ThenThrowEntityNotFoundException()
        {
            Setup();
            //given
            var password = "123456789";
            var clientExpected = new Client()
            {
                Name = "Misha",
                LastName = "Mahnov",
                Email = "mahnat@fja.com",
                Password = PasswordHash.HashPassword("123456789"),
                IsDeleted = true
            };

            _clientsRepositoryMock.Setup(c => c.GetClientByEmail(clientExpected.Email)).Returns(clientExpected);

            //when, then
            Assert.Throws<NotFoundException>(() => _sut.GetUserForLogin(clientExpected.Email, password));

        }
        
        [Test]
        public void Login_UserNotFound_ThenThrowEntityNotFoundException()
        {
            Setup();
            //given
            var badEmail = "ad@mmm.com";
            var clientExpected = new Client()
            {
                Id = 1,
                Name = "Andrey",
                LastName = "Chernuk",
                Password = "123456789",
                Email = "Chernuk@gmail.com",
            };

            //when, then
            Assert.Throws<NotFoundException>(() => _sut.GetUserForLogin(badEmail, clientExpected.Password));
        }
        
        [Test]
        public void GetToken_ValidData_ThenTokenReturned()
        {
            Setup();
            //given
            var model = new User()
            {
                Email = "ilyaIlich@mail.ru",
                Role = "Client"
            };

            //when
            var actual = _sut.GetToken(model);

            //then

            Assert.NotNull(actual);

        }
        
        [Test]
        public void GetToken_EmailEmpty_ThenThrowDataException()
        {
            Setup();
            //given
            var model = new User()
            {
                Email = null,
                Role = "Sitter"
            };

            //when, then
            Assert.Throws<DataException>(() => _sut.GetToken(model));

        }
        
        [Test]
        public void GetToken_RoleEmpty_ThenThrowDataException()
        {
            Setup();
            //given
            var model = new User()
            {
                Email = "ilyaIlich@mail.ru",
                Role = null
            };

            //when, then
            Assert.Throws<DataException>(() => _sut.GetToken(model));
        }

        [Test]
        public void GetToken_PropertysEmpty_ThenThrowDataException()
        {
            Setup();
            //given
            var model = new User();

            //when, then
            Assert.Throws<DataException>(() => _sut.GetToken(model));
        }
    }
}