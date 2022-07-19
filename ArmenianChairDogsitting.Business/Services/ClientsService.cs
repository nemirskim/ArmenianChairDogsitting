﻿using ArmenianChairDogsitting.Data.Repositories;
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

    public int AddClient(ClientModel clientModel)
    {
        var client = _mapper.Map<Client>(clientModel);
        client.Role = Role.Client;

        var id = _clientsRepository.AddClient(client);

        return id;
    }


    
}