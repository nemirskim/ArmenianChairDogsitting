using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Models;
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
            cfg.CreateMap<AbstractOrderRequest, OrderModel>().ReverseMap();
            cfg.CreateMap<OrderWalkRequest, OrderWalkModel>()
                .IncludeBase(typeof(AbstractOrderRequest), typeof(OrderModel))
                .ReverseMap();
            cfg.CreateMap<OrderDailySittingRequest, OrderDailySittingModel>()
                .IncludeBase(typeof(AbstractOrderRequest), typeof(OrderModel))
                .ReverseMap();
            cfg.CreateMap<OrderOverexposeRequest, OrderOverexposeModel>()
                .IncludeBase(typeof(AbstractOrderRequest), typeof(OrderModel))
                .ReverseMap();
            cfg.CreateMap<OrderSittingForDayRequest, OrderSittingForDayModel>()
                .IncludeBase(typeof(AbstractOrderRequest), typeof(OrderModel))
                .ReverseMap();

            cfg.CreateMap<CommentRequest, CommentModel>()
                .ForPath(d => d.Client.Id, opt => opt.MapFrom(src => src.ClientId))
                .ForPath(d => d.Order.Id, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(d => d.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(d => d.Rating, opt => opt.MapFrom(src => src.Rating));
        }));
    }
}
