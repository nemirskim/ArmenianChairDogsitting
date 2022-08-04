using ArmenianChairDogsitting.Data.Repositories;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Business.Hashing;

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

    public Client GetClientById(int id)
    {
        var client = _clientsRepository.GetClientById(id);
        if (client is null)
            throw new NotFoundException("Client was not found");

        return client;
    }

    public List<Client> GetAllClients()
    {
        var clients = _clientsRepository.GetAllClients();
        return clients;
    }

    public void UpdateClient(Client clientForUpdate, int id)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client is null)
            throw new NotFoundException("Client was not found");

        client.Phone = clientForUpdate.Phone;
        client.Name = clientForUpdate.Name;
        client.LastName = clientForUpdate.LastName;

        _clientsRepository.UpdateClient(client);
    }

    public void RemoveOrRestoreClient(int id, bool isDeleted)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client is null)
            throw new NotFoundException("Client was not found");

        else if (client.Role != Role.Admin || client.Id != id)
            throw new AccessDeniedException("Access denied");

        client.IsDeleted = isDeleted;
        _clientsRepository.RemoveOrRestoreClient(client);
    }

    private bool CheckEmailForExisting(string email) => _clientsRepository.GetClientByEmail(email) == null;
}
