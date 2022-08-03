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

    private bool CheckEmailForExisting(string email) => _clientsRepository.GetClientByEmail(email) == null;
}