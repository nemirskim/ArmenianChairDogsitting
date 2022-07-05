

using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ArmenianChairDogsittingContext _context;

        public CommentsRepository(ArmenianChairDogsittingContext context)
        {
            _context = context;
        }

        public int AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return comment.Id;
        }

        public List<Comment> GetAllComments() => _context.Comments.ToList();

        public Comment? GetCommentById(int id) =>
        _context.Comments.FirstOrDefault(o => o.Id == id);
            
    }
}
