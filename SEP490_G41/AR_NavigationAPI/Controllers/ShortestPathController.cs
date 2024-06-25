using DataAccess.IRepository;
using DataAccess.IRepository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace AR_NavigationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShortestPathController : ODataController
    {
        private readonly IMapPointRepository _mapPointRepository;

        public ShortestPathController(IMapPointRepository mapPointRepository)
        {
            _mapPointRepository = mapPointRepository;
        }

        [HttpGet("{startPointId}/{endPointId}")]
        [EnableQuery]
        public async Task<IActionResult> GetShortestPath(int startPointId, int endPointId)
        {
            var shortestPath = _mapPointRepository.GetAllMapPointsPath(startPointId, endPointId);

            if (shortestPath == null || shortestPath.Count == 0)
            {
                return NotFound("No path found between the specified points.");
            }

            return Ok(shortestPath);
        }

    }
}
