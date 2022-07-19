using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business;
using AutoMapper;

namespace ArmenianChairDogsitting.API;

public class MapperConfigStorage : Profile
{
    public MapperConfigStorage()
    {
        CreateMap<ClientRegistrationRequest, ClientModel>();
        CreateMap<ClientUpdateRequest, ClientModel>();        
        CreateMap<ClientModel, ClientAllInfoResponse>();
    }
}
