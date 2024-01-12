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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(AppUser user)
        {
            throw new NotImplementedException();
        }

        public bool AddFollow(Follow follow)
        {
            _context.Follows.Add(follow);
            return Save();
        }

        public bool DeleteFollow(Follow follow)
        {
            _context.Follows.Remove(follow);
            return Save();
        }

        public bool Delete(AppUser user)
        {
            _context.Users.Remove(user);
            return Save();
        }

        public async Task<Follow> IsUserFollow(string userId, string followingId)
        {
            return await _context.Follows
                    .Where(f => f.UserId == userId && f.FollowingId == followingId)
                    .FirstOrDefaultAsync();
        }
        public async Task<List<Follow>> GetAllFollowers(string userId)
        {
            return await _context.Follows
                    .Where(f => f.FollowingId == userId)
                    .Include(f => f.User)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<List<Follow>> GetAllFollowings(string userId)
        {
            return await _context.Follows
                    .Where(f => f.UserId == userId)
                    .Include(f => f.Following)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.Address).ToListAsync();
        }

        public async Task<AppUser?> GetUserByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser?> GetUserByIdAsyncWithNoTracking(string id)
        {
            
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }
    }
}