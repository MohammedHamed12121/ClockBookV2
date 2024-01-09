using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clockbook.Services.ViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? ImageUrl { get; set; }
        public string? Job { get; set; }
    }
}