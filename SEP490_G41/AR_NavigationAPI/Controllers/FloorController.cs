using BusinessObject.DTO;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Collections.Generic;

namespace AR_NavigationAPI.Controllers
{
    [Route("api/floors")]
    [ApiController]
    public class FloorsController : ODataController
    {
        private readonly IFloorRepository _floorRepository;

        public FloorsController(IFloorRepository floorRepository)
        {
            _floorRepository = floorRepository;
        }

               // GET: api/floors
        [HttpGet]
        [EnableQuery]
        public IQueryable<FloorDTO> GetAllFloors()
        {
            var floors = _floorRepository.GetAllFloors();
            return floors.AsQueryable(); // Chuyển đổi sang IQueryable
        }

        // GET: api/floors/5
        [HttpGet("{id}")]
        public IActionResult GetFloorById(int id)
        {
            var floor = _floorRepository.GetFloorById(id);

            if (floor == null)
            {
                return NotFound();
            }

            return Ok(floor);
        }

        [HttpGet("building/{buildingId}")]
        public IActionResult GetAllFloorsByBuildingId(int buildingId)
        {
            var floors = _floorRepository.GetAllFloorsByBuildingId(buildingId);
            return Ok(floors);
        }


        [HttpPost]
        public ActionResult AddFloors(List<FloorAddDTO> floors)
        {
            foreach (var floor in floors)
            {
                _floorRepository.AddFloor(floor);
            }

            return Ok("Floors added successfully");
        }

      /*  // PUT: api/floors/5
        [HttpPut("{id}")]
        public IActionResult UpdateFloorById(int id, FloorDTO floor)
        {
            var tmpFloor = _floorRepository.GetFloorById(id);
            if (tmpFloor == null)
            {
                return NotFound();
            }
            _floorRepository.UpdateFloor(floor);

            return Ok();
        }

        // DELETE: api/floors/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFloorById(int id)
        {
            var floor = _floorRepository.GetFloorById(id);

            if (floor == null)
            {
                return NotFound();
            }

            _floorRepository.DeleteFloor(id);

            return Ok();
        }*/
    }
}
