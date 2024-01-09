using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clockbook.Domain.Models;

namespace Clockbook.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsByPost(string id);
        bool Add(Comment comment);
        bool Update(Comment comment);
        bool Delete(Comment comment);
        bool Save();
    }
}