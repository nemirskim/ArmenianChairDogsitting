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
            cfg.CreateMap<Client, ClientModel>().ReverseMap();
            cfg.CreateMap<Animal, AnimalModel>().ReverseMap();
            cfg.CreateMap<Order, OrderModel>().ReverseMap();
            cfg.CreateMap<OrderWalk, OrderWalkModel>()
                .IncludeBase(typeof(Order), typeof(OrderModel))
                .ReverseMap();
            cfg.CreateMap<OrderDailySitting, OrderDailySittingModel>()
                .IncludeBase(typeof(Order), typeof(OrderModel))
                .ReverseMap();
            cfg.CreateMap<OrderOverexpose, OrderOverexposeModel>()
                .IncludeBase(typeof(Order), typeof(OrderModel))
                .ReverseMap();
            cfg.CreateMap<OrderSittingForDay, OrderSittingForDayModel>()
                .IncludeBase(typeof(Order), typeof(OrderModel))
                .ReverseMap();
            cfg.CreateMap<Comment, CommentModel>().ReverseMap();
        }));
    }
}