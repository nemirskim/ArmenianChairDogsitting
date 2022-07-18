using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories;

public class CommentsRepository : ICommentsRepository
{
    private readonly ArmenianChairDogsittingContext _context;

    public CommentsRepository(ArmenianChairDogsittingContext context)
    {
        _context = context;
    }

    public Comment? GetCommentById(int id) => _context.Comments.FirstOrDefault();


    public void DeleteCommentById(int id)
    {
        var choosenComment = _context.Comments
            .Where(o => !o.IsDeleted)
            .FirstOrDefault(c => c.Id == id);

        choosenComment!.IsDeleted = true;
        choosenComment.TimeUpdated = DateTime.Now;
        _context.Comments.Update(choosenComment);
        _context.SaveChanges();
    }           
}
