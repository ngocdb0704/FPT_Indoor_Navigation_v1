using BusinessObject.DTO;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System;

namespace AR_NavigationAPI.Controllers
{
    [Route("api/edges")]
    [ApiController]
    public class EdgesController : ODataController
    {
        private readonly IEdgeRepository _edgeRepository;

        public EdgesController(IEdgeRepository edgeRepository)
        {
            _edgeRepository = edgeRepository ?? throw new ArgumentNullException(nameof(edgeRepository));
        }

        // GET: api/edges
        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllEdges()
        {
            var edges = _edgeRepository.GetAllEdges();
            return Ok(edges);
        }

        // GET: api/edges/5
        [HttpGet("{id}")]
        public IActionResult GetEdgeById(int id)
        {
            var edge = _edgeRepository.GetEdgeById(id);

            if (edge == null)
            {
                return NotFound();
            }

            return Ok(edge);
        }

        // POST: api/edges
        [HttpPost]
        public ActionResult<EdgeAddDTO> AddEdge([FromBody] EdgeAddDTO edge)
        {
            _edgeRepository.AddEdge(edge);
            return Ok(edge);
        }

        // PUT: api/edges/5
        [HttpPut("{id}")]
        public IActionResult UpdateEdgeById(int id, [FromBody] EdgeUpdateDTO edge)
        {
            var tmpEdge = _edgeRepository.GetEdgeById(id);
            if (tmpEdge == null)
            {
                return NotFound();
            }
            _edgeRepository.UpdateEdge(edge);

            return Ok();
        }

        // DELETE: api/edges/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEdgeById(int id)
        {
            var edge = _edgeRepository.GetEdgeById(id);

            if (edge == null)
            {
                return NotFound();
            }

            _edgeRepository.DeleteEdge(id);

            return Ok();
        }
    }
}
