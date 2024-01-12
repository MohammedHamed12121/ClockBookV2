using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Clockbook.Domain.Models
{
    [PrimaryKey(nameof(UserId), nameof(FollowingId))]
    public class Follow
    {
        public string? UserId { get; set; }
        public AppUser? User { get; set; }

        public string? FollowingId { get; set; }
        public AppUser? Following { get; set; }
    }
}