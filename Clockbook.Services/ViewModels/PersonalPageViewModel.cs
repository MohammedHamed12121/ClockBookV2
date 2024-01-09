using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clockbook.Domain.Models;

namespace Clockbook.Services.ViewModels
{
    public class PersonalPageViewModel
    {
        public AppUser? User { get; set; }
        public List<Post>? Posts { get; set; }
    }
}