using BusinessObject.DTO;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace AR_NavigationAPI.Controllers
{
    [Route("api/facilities")]
    [ApiController]
    public class FacilitiesController : ODataController
    {
        private readonly IFacilityRepository _facilityRepository;

        public FacilitiesController(IFacilityRepository facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }

        // GET: api/facilities
        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllFacilities()
        {
            var facilities = _facilityRepository.GetAllFacilities();
            return Ok(facilities);
        }

        // GET: api/facilities/5
        [HttpGet("{id}")]
        public IActionResult GetFacilityById(int id)
        {
            var facility = _facilityRepository.GetFacilityById(id);

            if (facility == null)
            {
                return NotFound();
            }

            return Ok(facility);
        }

        // POST: api/facilities
        [HttpPost]
        public ActionResult<FacilityAddDTO> CreateFacility([FromBody] FacilityAddDTO facility)
        {
            _facilityRepository.AddFacility(facility);
            return Ok(facility);
        }

       /* // PUT: api/facilities/5
        [HttpPut("{id}")]
        public IActionResult UpdateFacility(int id, [FromBody] FacilityUpdateDTO facility)
        {
            var existingFacility = _facilityRepository.GetFacilityById(id);
            if (existingFacility == null)
            {
                return NotFound();
            }
            _facilityRepository.UpdateFacility(facility);

            return Ok();
        }

        // DELETE: api/facilities/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFacility(int id)
        {
            var facility = _facilityRepository.GetFacilityById(id);

            if (facility == null)
            {
                return NotFound();
            }

            _facilityRepository.DeleteFacility(id);

            return Ok();
        }*/
    }
}
