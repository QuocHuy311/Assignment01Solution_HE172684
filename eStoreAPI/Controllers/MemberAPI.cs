using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class MemberAPI : ControllerBase
    {
        private readonly IMemberRepository memberRepository = new MemberRepository();
        private readonly IConfiguration _configuration;

        public MemberAPI(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

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

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            var adminEmail = _configuration["AdminAccount:Email"];
            var adminPassword = _configuration["AdminAccount:Password"];

            if (loginDto.Email == adminEmail && loginDto.Password == adminPassword)
            {
                return Ok(new { Role = "Admin" });
            }

            var member = memberRepository.GetMembers()
                            .FirstOrDefault(m => m.Email == loginDto.Email && m.Password == loginDto.Password);

            if (member != null)
            {
                return Ok(new { Role = "Member", MemberId = member.MemberId });
            }

            return Unauthorized("Invalid email or password");
        }

    }
}
