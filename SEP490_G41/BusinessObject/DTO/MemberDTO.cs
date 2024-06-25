using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class MemberDTO
    {
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
    }

    public class AddMemberDTO
    {
        public string FullName { get; set; } = null!;
        public DateTime DoB { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int RoleId { get; set; }
    }
    public class MemberUpdateDTO
    {
        public int MemberId { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime DoB { get; set; } 
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Country { get; set; } = null!;
        public IFormFile Avatar { get; set; } = null!;

    }

    public class MemberStatusDTO
    {
        public int MemberId { get; set; }
        public string Status { get; set; } = null!;

    }
    public class ChangePasswordModel
    {
        public int MemberId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ReNewPassword { get; set; }
    }

}
