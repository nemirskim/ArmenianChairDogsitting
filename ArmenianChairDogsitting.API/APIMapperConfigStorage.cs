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

        CreateMap<Order, OrderResponse>()
            .ForMember(d => d.ClientId, opt => opt.MapFrom(s => s.Client.Id))
            .ForMember(d => d.SitterId, opt => opt.MapFrom(s => s.Sitter.Id));

        CreateMap<OrderRequest, Order>()
            .ForPath(d => d.Client.Id, opt => opt.MapFrom(s => s.ClientId))
            .ForPath(d => d.Sitter.Id, opt => opt.MapFrom(s => s.SitterId));
        CreateMap<OrderWalkRequest, Order>()
            .ForPath(d => d.Client.Id, opt => opt.MapFrom(s => s.ClientId))
            .ForPath(d => d.Sitter.Id, opt => opt.MapFrom(s => s.SitterId))
            .IncludeBase(typeof(OrderRequest), typeof(Order));
        CreateMap<OrderOverexposeRequest, Order>()
            .ForPath(d => d.Client.Id, opt => opt.MapFrom(s => s.ClientId))
            .ForPath(d => d.Sitter.Id, opt => opt.MapFrom(s => s.SitterId))
            .IncludeBase(typeof(OrderRequest), typeof(Order));
        CreateMap<OrderDailySittingRequest, Order>()
            .ForPath(d => d.Client.Id, opt => opt.MapFrom(s => s.ClientId))
            .ForPath(d => d.Sitter.Id, opt => opt.MapFrom(s => s.SitterId))
            .IncludeBase(typeof(OrderRequest), typeof(Order));
        CreateMap<OrderSittingForDayRequest, Order>()
            .ForPath(d => d.Client.Id, opt => opt.MapFrom(s => s.ClientId))
            .ForPath(d => d.Sitter.Id, opt => opt.MapFrom(s => s.SitterId))
            .IncludeBase(typeof(OrderRequest), typeof(Order));

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
        CreateMap<DogRequest, Animal>();

    }
}