using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clockbook.Domain.Data;
using Clockbook.Domain.Interfaces;
using Clockbook.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Clockbook.Services.Repositories
{
    public class PostRepositoy : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepositoy(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts
                            .Include(p => p.User)
                            .Include(p => p.Comments)
                                .ThenInclude(c => c.User)
                            .AsNoTracking()
                            .ToListAsync();
        }

        public async Task<List<Post>> GetPostByUserIdAsync(string id)
        {
            return await _context.Posts
                                .Where(p => p.UserId == id)
                                .Include(p => p.User)
                                .Include(p => p.Comments)
                                    .ThenInclude(c => c.User)
                                .AsNoTracking() 
                                .ToListAsync();
        }

        public bool Add(Post post)
        {
            _context.Posts.Add(post);
            return Save();
        }

        public bool Delete(Post post)
        {
            _context.Posts.Remove(post);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false ;
        }

        public bool Update(Post post)
        {
            _context.Posts.Update(post);
            return Save();
        }

    }
}