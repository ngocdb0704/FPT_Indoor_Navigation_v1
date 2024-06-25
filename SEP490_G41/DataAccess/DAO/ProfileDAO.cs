using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObject.Models;
using BusinessObject.Validate;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    

    public class ProfileDAO
    {
        private readonly finsContext _context;
        Validate validate = new Validate();
        public ProfileDAO(finsContext context)
        {
            _context = context;
        }

        // Đọc thông profile members nhà bằng Id
        public Member GetMemberById(int memberid)
        {
           var member =  _context.Members.FirstOrDefault(m => m.MemberId == memberid);
            return member;
        }

        // Cập nhật thông tin Profile
        public void UpdateProfile(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            var existingMember = _context.Members.FirstOrDefault(m => m.MemberId == member.MemberId);

            if (existingMember != null)
            {
                existingMember.MemberId = member.MemberId;
                existingMember.FullName = member.FullName;
                existingMember.DoB = member.DoB;
                existingMember.Address = member.Address;
                existingMember.Phone = member.Phone;
                existingMember.Email = member.Email;
                existingMember.Country = member.Country;
                existingMember.Avatar = member.Avatar;
                _context.SaveChanges();
                _context.Dispose();

            }
        }
       

    }
}
