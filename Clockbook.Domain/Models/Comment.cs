using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clockbook.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public string? PostId { get; set; }
        public Post? Post { get; set; }
    }
}