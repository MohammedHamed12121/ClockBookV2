using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Clockbook.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        
        public string? Hobbies { get; set; }
        public string? PreferedQoutes { get; set; }
        public string? ImageUrl { get; set; }
        public string? Job { get; set; }
        public string? AddressId { get; set; }
        public Address? Address { get; set; }
    }
}