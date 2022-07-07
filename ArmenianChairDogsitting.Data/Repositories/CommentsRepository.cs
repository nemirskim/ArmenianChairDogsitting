

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

        public void DeleteCommentById(int id)
        {
            var choosenComment = _context.Comments.Where(o => !o.IsDeleted).FirstOrDefault(c => c.Id == id);
            choosenComment.IsDeleted = true;
            choosenComment.TimeUpdated = DateTime.Now;
            _context.Comments.Update(choosenComment);
            _context.SaveChanges();
        }

        public List<Comment> GetAllComments() => _context.Comments.Where(o => !o.IsDeleted).ToList();
            
    }
}
