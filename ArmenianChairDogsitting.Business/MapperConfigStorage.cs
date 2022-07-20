using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Data.Entities;
using AutoMapper;

namespace ArmenianChairDogsitting.Business;

public class MapperConfigStorage : Profile
{
    public MapperConfigStorage()
    {
        CreateMap<IEnumerable<IEnumerable<Comment>>, IEnumerable<Comment>>()
            .ForMember(dest => dest.FirstOrDefault() != null ? dest.First().Text : ""
            , opt => opt.MapFrom(src =>
            src.FirstOrDefault() != null ? src.First().FirstOrDefault() != null ?
            src.First().First().Text : "" : ""));
            //.ForPath(dest => dest.FirstOrDefault().Rating 
            //, opt => opt.MapFrom(src => src.FirstOrDefault().FirstOrDefault().Rating))
            //.ForPath(dest => dest.FirstOrDefault().Text
            //, opt => opt.MapFrom(src => src.FirstOrDefault().FirstOrDefault().Text));


        CreateMap<Sitter, SittersSearchModelResult>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Orders.Select(t => t.Comments).ToList()));
    }
}