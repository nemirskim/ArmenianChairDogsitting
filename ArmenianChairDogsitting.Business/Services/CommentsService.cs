using ArmenianChairDogsitting.Data.Repositories;
using ArmenianChairDogsitting.Business.ExceptionsStorage;
using ArmenianChairDogsitting.Business.Interfaces;

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
            throw new NotFoundException($"{ExceptionMessage.ChoosenCommentDoesNotExist}{id}");
        }

        _commentsRepository.DeleteCommentById(id);
    }
}
