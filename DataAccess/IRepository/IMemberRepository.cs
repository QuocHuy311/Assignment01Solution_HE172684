using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IMemberRepository
    {
        List<Member> GetMembers();
        Member GetMemberById(int id);
        void InsertMember(Member p);
        void UpdateMember(Member p);
        void DeleteMember(int id);
    }
}
