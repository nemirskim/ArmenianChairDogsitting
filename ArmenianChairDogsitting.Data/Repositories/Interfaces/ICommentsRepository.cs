using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories
{
    public interface ICommentsRepository
    {
        void DeleteCommentById(int id);
        Comment GetCommentById(int id);
    }
}
