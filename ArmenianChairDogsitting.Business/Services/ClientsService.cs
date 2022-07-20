using ArmenianChairDogsitting.Data.Repositories;
using ArmenianChairDogsitting.Data.Entities;
using AutoMapper;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Business;

public class ClientsService : IClientsService
{
    private readonly IClientsRepository _clientsRepository;
    private readonly IMapper _mapper;

    public ClientsService(IClientsRepository clientsRepository, IMapper mapper)
    {
        _clientsRepository = clientsRepository;
        _mapper = mapper;
    }

    public int AddClient(Client clientModel)
    {
        var client = _mapper.Map<Client>(clientModel);
        client.Role = Role.Client;

        var id = _clientsRepository.AddClient(client);

        return id;
    }

    public Client GetClientById(int id)
    {
        var client = _clientsRepository.GetClientById(id);
        if (client is null)
            throw new NotFoundException("Client was not found");

        return _mapper.Map<Client>(client);
    }

    public List<Client> GetAllClients()
    {
        var clients = _clientsRepository.GetAllClients();
        return _mapper.Map<List<Client>>(clients);
    }

    public void UpdateClient(Client client, int id)
    {
        if (client is null)
            throw new NotFoundException("Client was not found");

        _clientsRepository.UpdateClient(client);
        _mapper.Map<Client>(client);
    }

    public void RemoveOrRestoreClient(int id, bool isDeleted)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client is null)
            throw new NotFoundException("Client was not found");

        _clientsRepository.RemoveOrRestoreClient(id, isDeleted);   
    } 
}
