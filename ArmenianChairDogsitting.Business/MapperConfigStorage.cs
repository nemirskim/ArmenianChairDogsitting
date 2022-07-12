using AutoMapper;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Business.Models;

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
            cfg.CreateMap<Client, ClientModel>();
            cfg.CreateMap<OrderWalk, OrderWalkModel>();
            cfg.CreateMap<Comment, CommentModel>()
               .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
               .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order));
        }));
    }
}