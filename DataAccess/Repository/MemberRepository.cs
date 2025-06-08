using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public List<Member> GetMembers() => MemberDAO.Instance.GetMemberList();

        public Member GetMemberById(int id) => MemberDAO.Instance.GetMemberById(id);

        public void InsertMember(Member p) => MemberDAO.Instance.AddMember(p);

        public void UpdateMember(Member p) => MemberDAO.Instance.UpdateMember(p);

        public void DeleteMember(int id) => MemberDAO.Instance.DeleteMember(id);
    }
}
