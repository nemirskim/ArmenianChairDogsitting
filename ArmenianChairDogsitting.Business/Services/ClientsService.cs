using ArmenianChairDogsitting.Data.Repositories;
using ArmenianChairDogsitting.Data.Entities;
using AutoMapper;
using ArmenianChairDogsitting.Data.Enums;

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
        client.Role = Role.Client;

        var id = _clientsRepository.AddClient(client);

        return id;
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

        _clientsRepository.UpdateClient(client);
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
}
