using BusinessObject.DTO;
using BusinessObject.Validate;
using DataAccess.IRepository;
using DataAccess.IRepository.Repository;
using K4os.Compression.LZ4.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace AR_NavigationAPI.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    public class ProfileController : ODataController
    {
        private readonly IProfileRepository _profileRepository;
        Validate validate = new Validate();
        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        // GET: api/buildings/1
        [HttpGet("{id}")]
        public IActionResult GetMemberById(int id)
        {
            var profile = _profileRepository.GetMemberById(id);

            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }
        // PUT: api/buildings/1
        [HttpPut]
        public IActionResult UpdateProfileById([FromForm] MemberUpdateDTO progfile)
        {
            _profileRepository.UpdateProfile(progfile);

            return Ok();
        }
   

    }
}
