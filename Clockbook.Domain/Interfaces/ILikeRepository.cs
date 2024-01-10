using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clockbook.Domain.Models;

namespace Clockbook.Domain.Interfaces
{
    public interface ILikeRepository
    {
        bool IsLiked(string userId, string id);
        Task<Like> GetTheLikeByPostIdAsync(string postId, string userId);
        bool Add(Like like);
        bool Delete(Like like);
        bool Save();
    }
}