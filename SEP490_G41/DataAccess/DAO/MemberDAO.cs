using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Validate;
using MySqlX.XDevAPI;
using System.Xml.Linq;
using BusinessObject.DTO;

namespace DataAccess.DAO
{
    public class MemberDAO
    {
        private readonly finsContext _context;
        Validate validate = new Validate();

        public MemberDAO(finsContext context)
        {
            _context = context;
        }
        #region Get all members
        public List<Member> GetAllMembers()
        {
            var lists = _context.Members.ToList();
            _context.Dispose();
            return lists;
        }
        #endregion

        #region Add new member
        //public void AddNewMember(Member member)
        //{
        //    _context.Members.Add(member);
        //    _context.SaveChanges();
        //    _context.Dispose();
        //}
        #endregion

        #region Delete member
        public bool DeleteMember(int id)
        {
            bool result = false;
            var member = _context.Members.FirstOrDefault(m => m.MemberId == id);
            if (member != null)
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
                result = true;
                _context.Dispose();
            }
            return result;
        }
        #endregion

        #region Login
        public Member Login(string username, string password)
        {
            var member = _context.Members.FirstOrDefault(m => m.Username.Equals(username) && m.Password.Equals(validate.EncodePassword(password)));
            if (member != null)
            {
                _context.Dispose();
                return member;
            }
            return null;
        }
        #endregion

        #region Search member by DoB
        public List<Member> SearchMemberByDoB(DateTime date)
        {
            var list = _context.Members.Where(m => m.DoB == date).ToList();
            _context.Dispose();
            return list;
        }
        #endregion

        #region Search member by name
        public List<Member> SearchMemberByName(string name)
        {
            if (name == null)
            {
                return _context.Members.ToList();
            }
            var list = _context.Members.Where(m => m.FullName.Contains(name)).ToList();
            _context.Dispose();
            return list;
        }
        #endregion

        public string SetMemberStatus()
        {
            return null;
        }
        #region Send code
        public string SendCode(string email)
        {
            var mem = _context.Members.FirstOrDefault(m => m.Email == email);
            if (mem != null)
            {
                // Thông tin tài khoản email gửi
                string emailFrom = "catminh2k1@gmail.com";
                string password = "xtgf kmfb bgtv rkqe";
                string code = validate.RandomCode();
                try
                {
                    // Tạo một đối tượng MailMessage
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(email);
                    mail.IsBodyHtml = true;
                    mail.Subject = "Reset Code";
                    mail.Body = @"This is your reset code: " + code;

                    // Cấu hình SMTP client
                    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com", 587);
                    smtpServer.Credentials = new NetworkCredential(emailFrom, password);
                    // Sử dụng SSL/TLS
                    smtpServer.EnableSsl = true;
                    // Gửi email
                    smtpServer.Send(mail);
                    _context.Dispose();
                    return code;
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
            }
            return null;
        }
        #endregion
        #region Get member by email
        public Member GetMemberByEmail(string email)
        {
            var member = _context.Members.FirstOrDefault(m => m.Email == email);
            if (member != null)
            {
                _context.Dispose();
                return member;
            }
            return null;
        }
        #endregion
        #region Reset password
        public bool ResetPassword(int id, string newpass)
        {
            var memEx = _context.Members.FirstOrDefault(m => m.MemberId == id);
            if (memEx != null)
            {
                memEx.Password = validate.EncodePassword(newpass);
                _context.SaveChanges();
                _context.Dispose();
                return true;
            }
            return false;
        }

        #endregion

        #region Change password
        public string ChangePassword(int id,ChangePasswordModel changePassword)
        {
            var mem = _context.Members.FirstOrDefault(m => m.MemberId == id && m.Password.Equals(validate.EncodePassword(changePassword.OldPassword)));
            if (mem != null)
            {
                if (changePassword.OldPassword != changePassword.NewPassword)
                {
                    if (changePassword.NewPassword == changePassword.ReNewPassword)
                    {
                        mem.Password = validate.EncodePassword(changePassword.NewPassword);
                        _context.SaveChanges();
                        _context.Dispose();
                        return "Success";
                    }
                    return "NotEqual";
                }
                return "Same";
            }
            return "Incorrect";
        }
        #endregion

        // Cập nhật thông tin Profile
        public void UpdateMemberStatus(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            var existingMember = _context.Members.FirstOrDefault(m => m.MemberId == member.MemberId);

            if (existingMember != null)
            {
                // Cập nhật trạng thái thành viên
                existingMember.Status = member.Status;

                _context.SaveChanges();
                _context.Dispose();
            }
        }
        
    }
}

