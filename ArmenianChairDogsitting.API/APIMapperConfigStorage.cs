using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Data.Entities;
using AutoMapper;

namespace ArmenianChairDogsitting.API;

public class APIMapperConfigStorage : Profile
{
    public APIMapperConfigStorage()
    {
        CreateMap<SitterRequest, Sitter>();

        CreateMap<SitterUpdateRequest, Sitter>();

        CreateMap<SitterAllInfoResponse, Sitter>();

        CreateMap<Sitter, SitterAllInfoResponse>();

        CreateMap<SitterMainInfoResponse, Sitter>();

        CreateMap<SitterUpdatePasswordRequest, Sitter>();

        CreateMap<Sitter, SitterMainInfoResponse>();
    }

}
