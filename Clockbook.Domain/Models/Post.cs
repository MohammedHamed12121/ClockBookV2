using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clockbook.Domain.Models
{
    public class Post
    {
        public string? Id { get; set; }
        public String? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}