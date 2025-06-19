using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class MemberAPI : ControllerBase
    {
        private readonly IMemberRepository memberRepository = new MemberRepository();

        [HttpGet]
        public IActionResult GetAllMember() => Ok(memberRepository.GetMembers());

        [HttpGet("{id}")]
        public IActionResult GetMemberById(int id)
        {
            var member = memberRepository.GetMemberById(id);
            return member == null ? NotFound() : Ok(member);
        }

        [HttpPost]
        public IActionResult AddMember(Member member)
        {
            memberRepository.InsertMember(member);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateMember(Member member)
        {
            memberRepository.UpdateMember(member);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            memberRepository.DeleteMember(id);
            return Ok();
        }
    }
}
