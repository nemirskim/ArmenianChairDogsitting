using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Data.Entities;
using AutoMapper;

namespace ArmenianChairDogsitting.Business;

public class MapperConfigStorage : Profile
{
    public MapperConfigStorage()
    {
        CreateMap<Sitter, SittersSearchModelResult>()
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .AfterMap((src, dest) => dest.Comments = new List<Comment>());        
    }
}