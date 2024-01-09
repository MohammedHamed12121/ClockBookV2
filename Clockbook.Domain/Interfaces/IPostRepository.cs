using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clockbook.Domain.Models;

namespace Clockbook.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<List<Post>> GetPostByUserIdAsync(string id);
        bool Add(Post post);
        bool Update(Post post);
        bool Delete(Post post);
        bool Save();
    }
}