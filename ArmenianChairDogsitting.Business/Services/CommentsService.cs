
using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;
using ArmenianChairDogsitting.Business.ExceptionsStorage;
using AutoMapper;
using ArmenianChairDogsitting.Business.Exceptions;

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
        if(comments.Count == 0 || comments is null)
        {
            throw new NotFoundException(CommentsExceptionStorage.NoCommentsYet);
        }
        return _map.Map(comments, new List<CommentModel>());
    }

    public void DeleteCommentById(int id)
    {
        var chosenComment = _commentsRepository.GetCommentById(id);

        if(chosenComment != null)
        {
            throw new NotFoundException($"{CommentsExceptionStorage.ChoosenCommentDoesNotExist}{id}");
        }

        _commentsRepository.DeleteCommentById(id);
    }

    public int AddComment(CommentModel comment)
    {
        var commentToAdd = _map.Map(comment, new Comment());
        return _commentsRepository.AddComment(commentToAdd);
    }
}
