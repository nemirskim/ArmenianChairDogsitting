
using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;
using AutoMapper;

namespace ArmenianChairDogsitting.Business.Services;

public class CommentsService
{
    CommentsRepository _commentsRepository;
    Mapper _map;

    public CommentsService(CommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
        _map = MapperConfigStorage.GetInstance();
    }

    public List<CommentModel> GetComments()
    {
        var comments = _commentsRepository.GetAllComments();
        return _map.Map(comments, new List<CommentModel>());
    }
}
