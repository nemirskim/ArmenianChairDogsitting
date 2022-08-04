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

    public void UpdateClient(Client client, int id)
    {
        if (client is null)
            throw new NotFoundException("Client was not found");

        _clientsRepository.UpdateClient(client, id);
    }

    public void RemoveOrRestoreClient(int id, bool isDeleted)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client is null)
            throw new NotFoundException("Client was not found");

        else if (client.Role != Role.Admin || client.Id != id)
            throw new AccessDeniedException("Access denied");

        else
        _clientsRepository.RemoveOrRestoreClient(id, isDeleted);
    }

    private bool CheckEmailForExisting(string email) => _clientsRepository.GetClientByEmail(email) == null;
}
