using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IProfileRepository
    {
        MemberDTO GetMemberById(int memberid);
        void UpdateProfile(MemberUpdateDTO profile);

   

    }
}
