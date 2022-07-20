using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Data.Entities;
using AutoMapper;

namespace ArmenianChairDogsitting.Business;

public class MapperConfigStorage : Profile
{
    public MapperConfigStorage()
    {
        CreateMap<IEnumerable<Comment>, IEnumerable<IEnumerable<Comment>>>();

        CreateMap<Sitter, SittersSearchModelResult>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Orders.Select(t => t.Comments).ToList()));
    }
}