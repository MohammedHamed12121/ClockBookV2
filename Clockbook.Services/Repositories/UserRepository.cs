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

        public bool Delete(AppUser user)
        {
            _context.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.Address).ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersByCityAsyncWithNoTracking(string city)
        {
            return await _context.Users.Where(u => u.Address.City == city).AsNoTracking().ToListAsync();
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