

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

        public void DelleteCommentById(int id)
        {
            var choosenComment = _context.Comments.FirstOrDefault(c => c.Id == id);
            choosenComment.IsDeleted = true;
            _context.Comments.Update(choosenComment);
            _context.SaveChanges();
        }

        public List<Comment> GetAllComments() => _context.Comments.ToList();

        public Comment? GetCommentById(int id) =>
        _context.Comments.FirstOrDefault(o => o.Id == id);
            
    }
}
