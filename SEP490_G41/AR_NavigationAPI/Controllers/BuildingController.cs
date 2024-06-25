using BusinessObject.DTO;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities.Collections;
using System.Collections.Generic;

namespace AR_NavigationAPI.Controllers
{
    [Route("api/buildings")]
    [ApiController]
    public class BuildingsController : ODataController
    {
        private readonly IBuildingRepository _buildingRepository;

        public BuildingsController(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        // GET: api/buildings
        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllBuildings()
        {
            var buildings = _buildingRepository.GetAllBuildings();
            return Ok(buildings);
        }

        // GET: api/buildings/5
        [HttpGet("{id}")]
        public IActionResult GetBuildingById(int id)
        {
            var building = _buildingRepository.GetBuildingById(id);

            if (building == null)
            {
                return NotFound();
            }

            return Ok(building);
        }

        // POST: api/buildings
        [HttpPost]
        public ActionResult<BuildingAddDTO> AddBuilding([FromForm] BuildingAddDTO building)
        {
            _buildingRepository.AddBuilding(building);
            return Ok(building);
        }

        // PUT: api/buildings/5
        [HttpPut]
        public IActionResult UpdateBuildingById([FromForm] BuildingUpdateDTO building)
        {
            
            _buildingRepository.UpdateBuilding(building);

            return Ok();
        }

        // DELETE: api/buildings/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBuildingById(int id)
        {
           

          string mess =  _buildingRepository.DeleteBuilding(id);

            return Ok();
        }
    }
}
