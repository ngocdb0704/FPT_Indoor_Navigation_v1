using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Member
    {
        public Member()
        {
            Mapmanages = new HashSet<Mapmanage>();
        }

        public int MemberId { get; set; }
        public int RoleId { get; set; }
        public string? FullName { get; set; }
        public DateTime? DoB { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? Avatar { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Mapmanage> Mapmanages { get; set; }
    }
}
