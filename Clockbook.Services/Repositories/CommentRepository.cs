using Clockbook.Domain.Data;
using Clockbook.Domain.Interfaces;
using Clockbook.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Clockbook.Services.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllCommentsByPost(string id)
        {
            return await _context.Comments
                            .Where(c => c.PostId == id)
                            .Include(c => c.User)
                            .AsNoTracking()
                            .ToListAsync();
        }

        public bool Add(Comment comment)
        {
            _context.Comments.Add(comment);
            return Save();
        }

        public bool Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
            return Save();
            
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 1;
        }

        public bool Update(Comment comment)
        {
            _context.Comments.Update(comment);
            return Save();
        }
    }
}