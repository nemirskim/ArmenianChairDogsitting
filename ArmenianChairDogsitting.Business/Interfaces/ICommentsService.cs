using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Business.Interfaces;

public interface ICommentsService
{
    public void DeleteCommentById(int id);
}
