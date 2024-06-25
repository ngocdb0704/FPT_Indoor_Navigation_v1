using Mysqlx.Session;
using NetTopologySuite.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessObject.Validate
{
    public class Validate
    {
        public bool CheckFullName(string fullname)
        {
            Regex regex = new Regex("^[a-zA-Z]+$");
            if (fullname != null && regex.IsMatch(fullname))
            {
                return true;
            }
            return false;
        }

        public bool CheckAddress(string address)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]+$");
            if (address != null && regex.IsMatch(address))
            {
                return true;
            }
            return false;
        }

        public bool CheckEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (email != null && regex.IsMatch(email))
            {
                return true;
            }
            return false;
        }
        
        public bool CheckPhoneNumber(string phone)
        {
            Regex regex = new Regex(@"^\d{10}$");
            if (phone != null && regex.IsMatch(phone))
            {
                return true;
            }
            return false;
        }

        public string RandomCode()
        {
            Random rd = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] code = new char[6];

            for (int i = 0; i < 6; i++)
            {
                code[i] = chars[rd.Next(chars.Length)];
            }
            return new string(code);

        }
        public string EncodePassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Mã hóa mật khẩu thành một mảng byte
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Chuyển đổi mảng byte thành chuỗi hex
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                // Chuỗi hex này là mật khẩu đã được mã hóa
                string encodePassword = builder.ToString();

                return encodePassword;
            }
            
        }
    }
}
