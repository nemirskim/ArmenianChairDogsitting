using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Data.Entities;
using AutoMapper;

namespace ArmenianChairDogsitting.Business;

public class MapperConfigStorage
{
    private static Mapper _instance;

    public static Mapper GetInstance()
    {
        if (_instance == null)
            InitializeInstance();
        return _instance!;
    }

    private static void InitializeInstance()
    {
        _instance = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<IEnumerable<Comment>, IEnumerable<IEnumerable<Comment>>>();

            cfg.CreateMap<Sitter, SittersSearchModelResult>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Orders.Select(t => t.Comments).ToList()));
        }));
    }
}