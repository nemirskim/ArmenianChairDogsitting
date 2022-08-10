using ArmenianChairDogsitting.Data.Repositories;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Business.Hashing;
using ArmenianChairDogsitting.Business.Exceptions;

namespace ArmenianChairDogsitting.Business;

public class ClientsService : IClientsService
{
    private readonly IClientsRepository _clientsRepository;

    public ClientsService(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public int AddClient(Client client)
    {
        var isExist = CheckEmailForExisting(client.Email);

        if (!isExist)
            throw new ExistingEmailException("This email already exists");

        else
            client.Password = PasswordHash.HashPassword(client.Password);
            client.Role = Role.Client;            

        return _clientsRepository.AddClient(client);
    }

    public Client GetClientById(int id) => _clientsRepository.GetClientById(id);
    public List<Client> GetAllClients() => _clientsRepository.GetAllClients();

    public void UpdateClient(Client clientForUpdate, int id)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client is null)
            throw new NotFoundException("Client was not found");

        client.Phone = clientForUpdate.Phone;
        client.Name = clientForUpdate.Name;
        client.LastName = clientForUpdate.LastName;
        client.Address = clientForUpdate.Address;

        _clientsRepository.UpdateClient(client);
    }

    public void RemoveOrRestoreClient(int id, bool isDeleted)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client is null)
            throw new NotFoundException("Client was not found");

        client.IsDeleted = isDeleted;
        _clientsRepository.RemoveOrRestoreClient(client);
    }

    private bool CheckEmailForExisting(string email) => _clientsRepository.GetClientByEmail(email) == null;

    public void UpdatePassword(int id, User clientForUpdate)
    {
        var sitter = _clientsRepository.GetClientById(id);

        if (sitter == null)
            throw new NotFoundException($"{ExceptionMessage.ChoosenSitterDoesNotExist}{id}");

        if (clientForUpdate.OldPassword == clientForUpdate.Password)
            throw new PasswordException($"{ExceptionMessage.OldPasswordEqualNew}");

        if (sitter.Password.Equals(PasswordHash.HashPassword(clientForUpdate.OldPassword)))
            throw new PasswordException($"{ExceptionMessage.OldPasswordDontEqualSitterPassword}");

        sitter.Password = PasswordHash.HashPassword(clientForUpdate.Password);

        _clientsRepository.UpdatePassword(sitter);
    }
}
