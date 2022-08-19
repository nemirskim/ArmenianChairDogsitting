using ArmenianChairDogsitting.Business.Hashing;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Data.Repositories;
using Moq;

namespace ArmenianChairDogsitting.Business.Tests;

public class ClientsServiceTests
{
    private Mock<Data.Repositories.IClientsService> _clientRepository;
    private ClientsService _sut;

    [SetUp]
    public void Setup()
    {
        _clientRepository = new Mock<Data.Repositories.IClientsService>();
        _sut = new ClientsService(_clientRepository.Object);
    }

    [Test]
    public void AddClient_WhenValidationPassed_ThenReturnIdOfAddedClient()
    {
        //given
        var expectedId = 1;

        var client = new Client()
        {
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Address = "Pirozhkovaia 28",
            Role = Role.Client,
            Dogs = new List<Animal>(),
            IsDeleted = false
        };

        _clientRepository.Setup(c => c.AddClient(client))
             .Returns(expectedId);

        //when
        var actual = _sut.AddClient(client);


        //then

        Assert.AreEqual(actual, expectedId);
        _clientRepository.Verify(c => c.AddClient(client), Times.Once);
    }

    [Test]
    public void GetClientById_WhenClientExist_ThenReturnClient()
    {
        //given
        var expectedClient = new Client()
        {
            Id = 1,
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Address = "Pirozhkovaia 28",
            Role = Role.Client,
            Dogs = new List<Animal>(),
            IsDeleted = false
        };

        _clientRepository
            .Setup(x => x.GetClientById(expectedClient.Id))
            .Returns(expectedClient);

        //when
        var actualClient = _sut.GetClientById(1);

        //then
        Assert.AreEqual(actualClient.Id, expectedClient.Id);
        Assert.AreEqual(actualClient.Name, expectedClient.Name);
        Assert.AreEqual(actualClient.LastName, expectedClient.LastName);
        Assert.AreEqual(actualClient.Email, expectedClient.Email);
        Assert.AreEqual(actualClient.Phone, expectedClient.Phone);
        Assert.AreEqual(actualClient.Address, expectedClient.Address);

        _clientRepository.Verify(x => x.GetClientById(expectedClient.Id), Times.Once);
    }

    [Test]
    public void GetClients_WhenExist_ThenReturnListClients()
    {
        //given
        List<Client> expectedClients = new List<Client>
        {
            new Client()
            {
                Id = 1,
                Name = "Alex",
                LastName = "Pistoletov",
                Phone = "89991116116",
                Email = "pistol@pi.com",
                Password = "123456789",
                Address = "Pirozhkovaia 28",
                Role = Role.Client,
                Dogs = new List<Animal>(),
                IsDeleted = false
            },
            new Client()
            {
                Id = 2,
                Name = "Pistolet",
                LastName = "Alexov",
                Phone = "89748274732",
                Email = "alex@pi.com",
                Password = "987654321",
                Address = "Pirozhkovaia 29",
                Role = Role.Client,
                Dogs = new List<Animal>(),
                IsDeleted = false
            },
            new Client()
            {
                Id = 3,
                Name = "Jhon",
                LastName = "Uick",
                Phone = "88005553535",
                Email = "microcredits@pi.com",
                Password = "1029384756",
                Address = "Pirozhkovaia 27",
                Role = Role.Client,
                Dogs = new List<Animal>(),
                IsDeleted = false
            }
        };

        _clientRepository
            .Setup(x => x.GetAllClients())
            .Returns(expectedClients);

        //when
        var actual = _sut.GetAllClients();

        //then
        Assert.True(actual is not null);
        Assert.AreEqual(actual.Count, expectedClients.Count);
        Assert.True(actual is List<Client>);
    }

    [Test]
    public void UpdateClient_WhenValidationPassed_ThenUpdateProperties()
    {
        //given

        var client = new Client()
        {
            Id = 10,
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Address = "Pirozhkovaia 28",
            Role = Role.Client,
            Dogs = new List<Animal>(),
            IsDeleted = false
        };


        var clientForUpdate = new Client()
        {
            Id = 9,
            Name = "Xela",
            LastName = "Avtomatov",
            Phone = "89998887766",
            Email = "avtom@pi.com",
            Password = "0987654321",
            Address = "Pirozhkovaia 40",
            Role = Role.Sitter,
            Dogs = new List<Animal>(),
            IsDeleted = true
        };

        _clientRepository.Setup(o => o.GetClientById(client.Id)).Returns(client);

        //when
        _sut.UpdateClient(clientForUpdate, client.Id);

        //then
        var actual = _sut.GetClientById(client.Id);

        _clientRepository.Verify(c => c.GetClientById(client.Id), Times.Exactly(2));
        _clientRepository.Verify(c => c.UpdateClient(It.Is<Client>(c =>
        c.IsDeleted == client.IsDeleted &&
        c.Name == clientForUpdate.Name &&
        c.LastName == clientForUpdate.LastName &&
        c.Phone == clientForUpdate.Phone &&
        c.Address == clientForUpdate.Address &&
        c.Role == client.Role &&
        c.Id == client.Id &&
        c.Password == client.Password &&
        c.Email == client.Email &&
        c.Dogs == client.Dogs
        )), Times.Once);
    }

    [Test]
    public void UpdateClient_WhenClientIsNotExist_ThenNotFoundExeption()
    {
        //given
        int clientId = 1;

        var client = new Client()
        {
            Id = 10,
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Address = "Pirozhkovaia 28",
            Role = Role.Client,
            Dogs = new List<Animal>(),
            IsDeleted = false
        };


        var clientForUpdate = new Client()
        {
            Id = 9,
            Name = "Xela",
            LastName = "Avtomatov",
            Phone = "89998887766",
            Email = "avtom@pi.com",
            Password = "0987654321",
            Address = "Pirozhkovaia 40",
            Role = Role.Sitter,
            Dogs = new List<Animal>(),
            IsDeleted = true
        };

        _clientRepository.Setup(o => o.GetClientById(client.Id)).Returns(client);

        //when
        //then
        Assert.Throws<NotFoundException>(() => _sut.UpdateClient(clientForUpdate, clientId));
    }

    [Test]
    public void RemoveOrRestoreById_WhenClientIsNotDeleted_ThenDeleteClient()
    {
        ///given
        var expectedClient = new Client()
        {
            Id = 10,
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Address = "Pirozhkovaia 28",
            Role = Role.Client,
            Dogs = new List<Animal>(),
            IsDeleted = false
        };

        _clientRepository.Setup(o => o.GetClientById(expectedClient.Id)).Returns(expectedClient);

        //when
        _sut.RemoveOrRestoreClient(expectedClient.Id, true);


        //then

        var allClient = _sut.GetAllClients();
        var actualClient = _sut.GetClientById(expectedClient.Id);

        Assert.True(allClient is null);
        Assert.True(actualClient.IsDeleted);

        _clientRepository.Verify(c => c.GetClientById(expectedClient.Id), Times.Exactly(2));
        _clientRepository.Verify(c => c.GetAllClients(), Times.Once);
    }

    [Test]
    public void RemoveOrRestoreById_WhenClientIsDeleted_ThenRestoreClient()
    {
        ///given
        var expectedClient = new Client()
        {
            Id = 10,
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Address = "Pirozhkovaia 28",
            Role = Role.Client,
            Dogs = new List<Animal>(),
            IsDeleted = true
        };

        _clientRepository.Setup(o => o.GetClientById(expectedClient.Id)).Returns(expectedClient);
        _clientRepository.Setup(o => o.GetAllClients()).Returns(new List<Client> { expectedClient });

        //when
        _sut.RemoveOrRestoreClient(expectedClient.Id, false);


        //then

        var allClient = _sut.GetAllClients();
        var actualClient = _sut.GetClientById(expectedClient.Id);

        Assert.True(allClient is not null);
        Assert.False(actualClient.IsDeleted);

        _clientRepository.Verify(c => c.GetClientById(expectedClient.Id), Times.Exactly(2));
        _clientRepository.Verify(c => c.GetAllClients(), Times.Once);
    }

    [Test]
    public void RemoveOrRestoreById_WhenClientIsNotExist_ThenNotFoundExeption()
    {
        //given
        int clientId = 1;

        var expectedClient = new Client()
        {
            Id = 10,
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Address = "Pirozhkovaia 28",
            Role = Role.Client,
            Dogs = new List<Animal>(),
            IsDeleted = true
        };

        _clientRepository.Setup(o => o.GetClientById(expectedClient.Id)).Returns(expectedClient);

        //then, when
        Assert.Throws<NotFoundException>(() => _sut.RemoveOrRestoreClient(clientId, true));
    }

    [Test]
    public void UpdatePassword_WhenValidationPassed_ThenUpdatePassword()
    {
        //given

        var client = new Client()
        {
            Id = 10,
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "1234567890",
            Address = "Kosoi pereylok 228",
            Dogs = new List<Animal>(),
            IsDeleted = false
        };


        var clientForUpdate = new User
        {
            Password = "0987654321",
            OldPassword = "1234567890"
        };

        _clientRepository.Setup(o => o.GetClientById(client.Id)).Returns(client);

        //when
        _sut.UpdatePassword(client.Id, clientForUpdate);

        //then
        var actual = _sut.GetClientById(client.Id);


        Assert.AreEqual(actual.Password, client.Password);

        _clientRepository.Verify(c => c.GetClientById(client.Id), Times.Exactly(2));
        _clientRepository.Verify(c => c.UpdatePassword(client), Times.Once);
    }

    [Test]
    public void UpdatePassword_WhenClientIsNotExist_ThenNotFoundExeption()
    {
        //given
        int clientId = 1;

        var client = new Client()
        {
            Id = 10,
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Address = "Kosoi pereylok 228",
            Dogs = new List<Animal>(),
            IsDeleted = false
        };


        var clientForUpdate = new User
        {
            Password = "0987654321",
            OldPassword = "1234567890"
        };

        _clientRepository.Setup(o => o.GetClientById(client.Id)).Returns(client);

        //when

        //then
        Assert.Throws<NotFoundException>(() => _sut.UpdatePassword(clientId, clientForUpdate));
    }

    [Test]
    public void UpdatePassword_WhenOldPasswordEqualNew_ThenPasswordException()
    {
        //given
        var client = new Client()
        {
            Id = 10,
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = "123456789",
            Address = "Kosoi pereylok 228",
            Dogs = new List<Animal>(),
            IsDeleted = false
        };


        var clientForUpdate = new User
        {
            Password = "1234567890",
            OldPassword = "1234567890"
        };

        _clientRepository.Setup(o => o.GetClientById(client.Id)).Returns(client);

        //when

        //then
        Assert.Throws<PasswordException>(() => _sut.UpdatePassword(client.Id, clientForUpdate));
    }

    [Test]
    public void UpdatePassword_WhenOldPasswordDontEqualActual_ThenPasswordException()
    {
        //given
        var client = new Client()
        {
            Id = 10,
            Name = "Alex",
            LastName = "Pistoletov",
            Phone = "89991116116",
            Email = "pistol@pi.com",
            Password = PasswordHash.HashPassword("123456789"),
            Address = "Kosoi pereylok 228",
            Dogs = new List<Animal>(),
            IsDeleted = false
        };


        var clientForUpdate = new User
        {
            Password = "0987654321",
            OldPassword = "123456578997"
        };

        _clientRepository.Setup(o => o.GetClientById(client.Id)).Returns(client);

        //when

        //then
        Assert.Throws<PasswordException>(() => _sut.UpdatePassword(client.Id, clientForUpdate));
    }
}
