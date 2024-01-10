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
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDbContext _context;

        public LikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Like like)
        {
            _context.Likes.Add(like);
            return Save();
        }

        public bool Delete(Like like)
        {
            _context.Likes.Remove(like);
            return Save();
        }

        public async Task<Like> GetTheLikeByPostIdAsync(string postId, string userId)
        {
            return await _context.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
                            
        }

        public bool IsLiked(string userId, string postId)
        {
           return _context.Likes.Any(l => l.UserId == userId && l.PostId == postId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}