using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Data.Entities;
using AutoMapper;

namespace ArmenianChairDogsitting.API;

public class APIMapperConfigStorage
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
            cfg.CreateMap<AbstractOrderRequest, Order>().ReverseMap();
            cfg.CreateMap<OrderWalkRequest, OrderWalk>()
                .IncludeBase(typeof(AbstractOrderRequest), typeof(Order))
                .ReverseMap();
            cfg.CreateMap<OrderDailySittingRequest, OrderDailySitting>()
                .IncludeBase(typeof(AbstractOrderRequest), typeof(Order))
                .ReverseMap();
            cfg.CreateMap<OrderOverexposeRequest, OrderOverexpose>()
                .IncludeBase(typeof(AbstractOrderRequest), typeof(Order))
                .ReverseMap();
            cfg.CreateMap<OrderSittingForDayRequest, OrderSittingForDay>()
                .IncludeBase(typeof(AbstractOrderRequest), typeof(Order))
                .ReverseMap();

            cfg.CreateMap<CommentRequest, Comment>();
        }));
    }
}
