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
            cfg.CreateMap<CommentRequest, CommentModel>().ReverseMap();
            cfg.CreateMap<CommentResponse, CommentModel>().ReverseMap();
        }));
    }
}
