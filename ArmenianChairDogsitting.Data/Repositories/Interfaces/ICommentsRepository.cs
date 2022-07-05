using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories
{
    public interface ICommentsRepository
    {
        List<Comment> GetAllComments();
        int AddComment(Comment comment);
        Comment GetCommentById(int id);
        void DelleteCommentById(int id);
    }
}
