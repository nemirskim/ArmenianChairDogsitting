using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Business.Interfaces;

public interface ICommentsService
{
    public List<Comment> GetComments();

    public void DeleteCommentById(int id);

    public int AddComment(Comment comment);
}
