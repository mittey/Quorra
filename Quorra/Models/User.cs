using System;
using System.ComponentModel.DataAnnotations;

namespace Quorra.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
