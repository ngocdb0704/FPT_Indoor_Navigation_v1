using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace AR_NavigationAPI.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class MemberController : ODataController
    {
        private readonly IMemberRepository _memberRepository;
        private readonly finsContext _finsContext;
        public MemberController(IMemberRepository memberRepository, finsContext finsContext)
        {
            _memberRepository = memberRepository;
            _finsContext = finsContext;
        }
        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllMembers()
        {
            var member = _memberRepository.GetAllMembers();
            return Ok(member);
        }


        [HttpGet("{name}")]
        [EnableQuery]
        public IActionResult SearchMemberByName(string name)
        {

            var members = _memberRepository.SearchMemberByName(name);

            return Ok(members);
        }

        [HttpPut("ChangePassword/{id}")]
        public IActionResult ChangePassword(int id, ChangePasswordModel changePassword)
        {
            var result = _memberRepository.ChangePassword(id, changePassword);
            if (result.Equals("Success"))
            {
                return Ok(result);
            }
            if (result.Equals("NotEqual"))
            {
                return Ok(result);
            }
            if (result.Equals("Same"))
            {
                return Ok(result);
            }
            return BadRequest("Password is incorrect!");
            
        }

        [HttpGet("Login")]
        public IActionResult Login(string username, string password)
        {
            var check = _memberRepository.Login(username, password);
            if (check != null)
            {
                return Ok(check);
            }
            return NotFound();
        }

        [HttpGet("CheckSession")]
        public IActionResult CheckSession([FromServices] IHttpContextAccessor httpContextAccessor)
        {
            // Lấy giá trị từ session
            var username = httpContextAccessor.HttpContext.Session.GetString("Username");
            var role = httpContextAccessor.HttpContext.Session.GetString("Role");

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(role))
            {
                // Session tồn tại
                return Ok(new { Username = username, Role = role });
            }
            else
            {
                // Session không tồn tại
                return NotFound();
            }
        }
        [HttpPut("updateStatus")]
        public IActionResult UpdateMemberStatus(MemberStatusDTO member)
        {
            try
            {
                _memberRepository.UpdateMemberStatus(member);
                return Ok("Member status updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating member status.");
            }
        }
        [HttpPut("ResetPassword/{id}")]
        public IActionResult ResetPassword(int id, string newpass)
        {
            var check = _memberRepository.ResetPassword(id, newpass);
            if (check == true)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("GetMemberByEmail")]
        public IActionResult GetMemberByEmail(string email)
        {
            var code = _memberRepository.GetMemberByEmail(email);
            if (code == null)
            {
                return BadRequest();
            }
            return Ok(code);
        }
        [HttpGet("GetCode")]
        public IActionResult GetCode(string email)
        {
            var code = _memberRepository.SendCode(email);
            if (code == null)
            {
                return BadRequest();
            }
            return Ok(code);
        }
    }
}
