using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clockbook.Domain.Models;

namespace Clockbook.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<AppUser?> GetUserByIdAsync(string id);
        Task<AppUser?> GetUserByIdAsyncWithNoTracking(string id);
        bool AddFollow(Follow follow);
        bool DeleteFollow(Follow follow);
        Task<Follow> IsUserFollow(string userId, string followingId);
        Task<List<Follow>> GetAllFollowers(string userId);
        Task<List<Follow>>GetAllFollowings(string userId);
        bool Add(AppUser user);

        bool Update(AppUser user);

        bool Delete(AppUser user);

        bool Save();
    }
}