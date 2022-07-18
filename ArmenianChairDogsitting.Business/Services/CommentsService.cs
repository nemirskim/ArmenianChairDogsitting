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

    public CommentsService(ICommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }
    public void DeleteCommentById(int id)
    {
        var chosenComment = _commentsRepository.GetCommentById(id);

        if(chosenComment is null)
        {
            throw new NotFoundException($"{ExceptionStorage.ChoosenCommentDoesNotExist}{id}");
        }

        _commentsRepository.DeleteCommentById(id);
    }
}
