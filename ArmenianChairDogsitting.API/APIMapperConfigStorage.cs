using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Data.Entities;
using AutoMapper;

namespace ArmenianChairDogsitting.API;

public class APIMapperConfigStorage : Profile
{
    public APIMapperConfigStorage()
    {
        CreateMap<SearchRequest, SearchParams>();
        CreateMap<SittersSearchModelResult, SitterAllInfoResponse>();
        CreateMap<AbstractOrderRequest, Order>().ReverseMap();
        CreateMap<AbstractOrderResponse, Order>().ReverseMap();

        CreateMap<OrderWalkRequest, OrderWalk>()
            .ForPath(d => d.Client.Id, opt => opt.MapFrom(s => s.ClientId))
            .ForPath(d => d.Sitter.Id, opt => opt.MapFrom(s => s.SitterId))
            .IncludeBase(typeof(AbstractOrderRequest), typeof(Order));

        CreateMap<OrderDailySittingRequest, OrderDailySitting>()
            .ForPath(d => d.Client.Id, opt => opt.MapFrom(s => s.ClientId))
            .ForPath(d => d.Sitter.Id, opt => opt.MapFrom(s => s.SitterId))
            .IncludeBase(typeof(AbstractOrderRequest), typeof(Order));

        CreateMap<OrderOverexposeRequest, OrderOverexpose>()
            .ForPath(d => d.Client.Id, opt => opt.MapFrom(s => s.ClientId))
            .ForPath(d => d.Sitter.Id, opt => opt.MapFrom(s => s.SitterId))
            .IncludeBase(typeof(AbstractOrderRequest), typeof(Order));

        CreateMap<OrderSittingForDayRequest, OrderSittingForDay>()
            .ForPath(d => d.Client.Id, opt => opt.MapFrom(s => s.ClientId))
            .ForPath(d => d.Sitter.Id, opt => opt.MapFrom(s => s.SitterId))
            .IncludeBase(typeof(AbstractOrderRequest), typeof(Order));

        CreateMap<OrderWalk, OrderWalkResponse>()
            .ForMember(d => d.ClientId, opt => opt.MapFrom(s => s.Client.Id))
            .ForMember(d => d.SitterId, opt => opt.MapFrom(s => s.Sitter.Id))
            .IncludeBase(typeof(Order), typeof(AbstractOrderResponse));
        CreateMap<OrderDailySitting, OrderDailySittingResponse>()

            .ForMember(d => d.ClientId, opt => opt.MapFrom(s => s.Client.Id))
            .ForMember(d => d.SitterId, opt => opt.MapFrom(s => s.Sitter.Id))
            .IncludeBase(typeof(Order), typeof(AbstractOrderResponse));

        CreateMap<OrderOverexpose, OrderOverexposeResponse>()
            .ForMember(d => d.ClientId, opt => opt.MapFrom(s => s.Client.Id))
            .ForMember(d => d.SitterId, opt => opt.MapFrom(s => s.Sitter.Id))
            .IncludeBase(typeof(Order), typeof(AbstractOrderResponse));

        CreateMap<OrderSittingForDay, OrderSittingForDayResponse>()
            .ForMember(d => d.ClientId, opt => opt.MapFrom(s => s.Client.Id))
            .ForMember(d => d.SitterId, opt => opt.MapFrom(s => s.Sitter.Id))
            .IncludeBase(typeof(Order), typeof(AbstractOrderResponse));

        CreateMap<Client, ClientAllInfoRequest>().ReverseMap();
        CreateMap<Client, ClientAllInfoResponse>().ReverseMap();

        CreateMap<CommentRequest, Comment>().ReverseMap();
    }
}