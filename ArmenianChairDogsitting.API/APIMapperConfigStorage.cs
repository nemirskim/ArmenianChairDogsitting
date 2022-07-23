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

        CreateMap<Sitter, SitterAllInfoResponse>();

        CreateMap<UserUpdatePasswordRequest, Sitter>();

        CreateMap<Sitter, SitterMainInfoResponse>();
    }

}
