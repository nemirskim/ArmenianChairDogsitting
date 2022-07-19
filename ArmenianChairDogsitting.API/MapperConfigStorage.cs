using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Data.Entities;
using AutoMapper;

namespace ArmenianChairDogsitting.API;

public class MapperConfigStorage : Profile
{
    public MapperConfigStorage()
    {
        CreateMap<ClientRegistrationRequest, Client>();
        CreateMap<ClientUpdateRequest, Client>();        
        CreateMap<Client, ClientAllInfoResponse>();
    }
}
