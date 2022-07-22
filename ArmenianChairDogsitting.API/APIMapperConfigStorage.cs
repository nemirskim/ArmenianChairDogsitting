using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Data.Entities;
using AutoMapper;

namespace ArmenianChairDogsitting.API;

public class APIMapperConfigStorage : Profile
{
    public APIMapperConfigStorage()
    {
        CreateMap<SearchRequest, SearchParams>();
        CreateMap<SittersSearchModelResult, SitterAllInfoResponse>();
    }
}