using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clockbook.Domain.Models;

namespace Clockbook.Services.ViewModels
{

    // Not used yet 
    public class PostsViewModel
    {
        public string? UserId {get; set;}
        public IEnumerable<Post>? Posts { get; set; }
        public bool IsUserLIked { get; set; }
    }
}