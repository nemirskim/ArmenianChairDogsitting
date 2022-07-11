using ArmenianChairDogsitting.Business.Models;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;
using ArmenianChairDogsitting.Business.ExceptionsStorage;
using ArmenianChairDogsitting.Business.Exceptions;
using ArmenianChairDogsitting.Business.Interfaces;
using AutoMapper;

namespace ArmenianChairDogsitting.Business.Services;

public class CommentsService : ICommentsService
{
    ICommentsRepository _commentsRepository;
    IMapper _mapper;

    public CommentsService(ICommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
        _mapper = MapperConfigStorage.GetInstance();
    }

    public List<CommentModel> GetComments()
    {
        var comments = _commentsRepository.GetAllComments();
        if(comments.Count == 0 || comments is null)
        {
            throw new NotFoundException(CommentsExceptionStorage.NoCommentsYet);
        }
        return _mapper.Map(comments, new List<CommentModel>());
    }

    public void DeleteCommentById(int id)
    {
        var chosenComment = _commentsRepository.GetCommentById(id);

        if(chosenComment is null)
        {
            throw new NotFoundException($"{CommentsExceptionStorage.ChoosenCommentDoesNotExist}{id}");
        }

        _commentsRepository.DeleteCommentById(id);
    }

    public int AddComment(CommentModel comment)
    {
        var commentToAdd = _mapper.Map(comment, new Comment());
        return _commentsRepository.AddComment(commentToAdd);
    }
}
