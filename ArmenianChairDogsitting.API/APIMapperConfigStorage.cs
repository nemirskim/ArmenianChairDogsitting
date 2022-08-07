using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Data;
using ArmenianChairDogsitting.Data.Entities;
using AutoMapper;

namespace ArmenianChairDogsitting.API;

public class APIMapperConfigStorage : Profile
{
    public APIMapperConfigStorage()
    {
        CreateMap<SearchRequest, ParamsToSearchSitter>();
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
        CreateMap<Client, ClientMainInfoResponse>();
        CreateMap<Client, ClientAllInfoResponse>();
        CreateMap<ClientRegistrationRequest, Client>();
        CreateMap<ClientUpdateRequest, Client>();
        CreateMap<Client, ClientMainInfoResponse>();

        CreateMap<CommentRequest, Comment>().ReverseMap();
        CreateMap<SitterRequest, Sitter>();
        CreateMap<SitterUpdatePriceCatalogRequest, Sitter>();
        CreateMap<AdminRequest, Admin>();
        CreateMap<PriceCatalogRequest, PriceCatalog>();
        CreateMap<PriceCatalog, PriceCatalogResponse>();
        CreateMap<SitterUpdateRequest, Sitter>();
        CreateMap<Sitter, SitterAllInfoResponse>();
        CreateMap<UserUpdatePasswordRequest, User>();
        CreateMap<ClientRegistrationRequest, Client>();
        CreateMap<Sitter, SitterMainInfoResponse>();
        CreateMap<UpdateOrderRequest, UpdateOrderModel>();
    }
}