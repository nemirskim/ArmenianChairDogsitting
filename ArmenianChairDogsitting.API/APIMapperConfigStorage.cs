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
            cfg.CreateMap<AbstractOrderResponse, Order>().ReverseMap();

            cfg.CreateMap<OrderWalkRequest, OrderWalk>()
                .ForPath(d => d.Client.Id, opt => opt.MapFrom(s => s.ClientId))
                .ForPath(d => d.Sitter.Id, opt => opt.MapFrom(s => s.SitterId))
                .IncludeBase(typeof(AbstractOrderRequest), typeof(Order));

            cfg.CreateMap<OrderDailySittingRequest, OrderDailySitting>()
                .ForPath(d => d.Client.Id, opt => opt.MapFrom(s => s.ClientId))
                .ForPath(d => d.Sitter.Id, opt => opt.MapFrom(s => s.SitterId))
                .IncludeBase(typeof(AbstractOrderRequest), typeof(Order));

            cfg.CreateMap<OrderOverexposeRequest, OrderOverexpose>()
                .ForPath(d => d.Client.Id, opt => opt.MapFrom(s => s.ClientId))
                .ForPath(d => d.Sitter.Id, opt => opt.MapFrom(s => s.SitterId))
                .IncludeBase(typeof(AbstractOrderRequest), typeof(Order));

            cfg.CreateMap<OrderSittingForDayRequest, OrderSittingForDay>()
                .ForPath(d => d.Client.Id, opt => opt.MapFrom(s => s.ClientId))
                .ForPath(d => d.Sitter.Id, opt => opt.MapFrom(s => s.SitterId))
                .IncludeBase(typeof(AbstractOrderRequest), typeof(Order));

            cfg.CreateMap<OrderWalk, OrderWalkResponse>()
                .ForMember(d => d.ClientId, opt => opt.MapFrom(s => s.Client.Id))
                .ForMember(d => d.SitterId, opt => opt.MapFrom(s => s.Sitter.Id))
                .IncludeBase(typeof(Order), typeof(AbstractOrderResponse));

            cfg.CreateMap<OrderDailySitting, OrderDailySittingResponse>()
                .ForMember(d => d.ClientId, opt => opt.MapFrom(s => s.Client.Id))
                .ForMember(d => d.SitterId, opt => opt.MapFrom(s => s.Sitter.Id))
                .IncludeBase(typeof(Order), typeof(AbstractOrderResponse));

            cfg.CreateMap<OrderOverexpose, OrderOverexposeResponse>()
                .ForMember(d => d.ClientId, opt => opt.MapFrom(s => s.Client.Id))
                .ForMember(d => d.SitterId, opt => opt.MapFrom(s => s.Sitter.Id))
                .IncludeBase(typeof(Order), typeof(AbstractOrderResponse));

            cfg.CreateMap<OrderSittingForDay, OrderSittingForDayResponse>()
                .ForMember(d => d.ClientId, opt => opt.MapFrom(s => s.Client.Id))
                .ForMember(d => d.SitterId, opt => opt.MapFrom(s => s.Sitter.Id))
                .IncludeBase(typeof(Order), typeof(AbstractOrderResponse));

            cfg.CreateMap<Client, ClientAllInfoRequest>().ReverseMap();
            cfg.CreateMap<Client, ClientAllInfoResponse>().ReverseMap();

            cfg.CreateMap<CommentRequest, Comment>().ReverseMap();
        }));
    }
}
