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
            cfg.CreateMap<Comment, CommentModel>().ReverseMap();
            cfg.CreateMap<CommentModel, Comment>().ReverseMap();
        }));
    }
}