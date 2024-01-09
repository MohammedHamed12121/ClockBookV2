using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clockbook.Domain.Models
{
    public class Address
    {
        public string Id { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int ZipCode  { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}