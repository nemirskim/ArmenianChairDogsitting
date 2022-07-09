using ArmenianChairDogsitting.Business.Models;

namespace ArmenianChairDogsitting.Business.Interfaces;

public interface ICommentsService
{
    public List<CommentModel> GetComments();

    public void DeleteCommentById(int id);

    public int AddComment(CommentModel comment);
}
