using BusinessObject.DTO;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Collections.Generic;

namespace AR_NavigationAPI.Controllers
{
    [Route("api/mappoints")]
    [ApiController]
    public class MapPointsController : ODataController
    {
        private readonly IMapPointRepository _mapPointRepository;

        public MapPointsController(IMapPointRepository mapPointRepository)
        {
            _mapPointRepository = mapPointRepository;
        }

        // GET: api/mappoints
        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllMapPoints()
        {
            var mapPoints = _mapPointRepository.GetAllMapPoints();
            return Ok(mapPoints);
        }

        // GET: api/mappoints/5
        [HttpGet("{id}")]
        public IActionResult GetMapPointById(int id)
        {
            if (id <= 0)
                return BadRequest("MapPoint ID must be a positive integer.");

            var mapPoint = _mapPointRepository.GetMapPointById(id);
            if (mapPoint == null)
                return NotFound();

            return Ok(mapPoint);
        }

        // POST: api/mappoints
        [HttpPost]
        public ActionResult<MapPointAddDTO> AddMapPoint([FromForm]MapPointAddDTO mapPoint)
        {
            if (mapPoint == null)
                return BadRequest("MapPoint cannot be null.");

            if (string.IsNullOrWhiteSpace(mapPoint.LocationWeb))
                return BadRequest("Location cannot be null or empty.");

            _mapPointRepository.AddMapPoint(mapPoint);
            return Ok(mapPoint);
        }

        // PUT: api/mappoints/5
        [HttpPut("{id}")]
        public IActionResult UpdateMapPointById(int id, [FromForm] MapPointUpdateDTO mapPoint)
        {
            if (id <= 0)
                return BadRequest("MapPoint ID must be a positive integer.");

            if (mapPoint == null)
                return BadRequest("MapPoint cannot be null.");

            if (string.IsNullOrWhiteSpace(mapPoint.LocationWeb))
                return BadRequest("Location cannot be null or empty.");

            _mapPointRepository.UpdateMapPoint(mapPoint);
            return Ok();
        }

        // DELETE: api/mappoints/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMapPointById(int id)
        {
            if (id <= 0)
                return BadRequest("MapPoint ID must be a positive integer.");
            _mapPointRepository.DeleteMapPoint(id);
            return Ok();
        }
    }
}