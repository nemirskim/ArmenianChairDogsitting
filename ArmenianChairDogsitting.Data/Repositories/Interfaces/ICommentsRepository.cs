using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories
{
    public interface ICommentsRepository
    {
        List<Comment> GetAllComments();
        int AddComment(Comment comment);
        void DeleteCommentById(int id);
        Comment GetCommentById(int id);
    }
}
