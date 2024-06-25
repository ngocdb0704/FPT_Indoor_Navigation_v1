using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository.Repository
{
    public class EdgeRepository : IEdgeRepository
    {
        private readonly EdgeDAO _edgeDAO;
        private readonly IMapper _mapper;

        public EdgeRepository(EdgeDAO edgeDAO, IMapper mapper)
        {
            _edgeDAO = edgeDAO;
            _mapper = mapper;
        }

        public EdgeDTO GetEdgeById(int edgeId)
        {
            var edge = _edgeDAO.GetEdgeDetailById(edgeId);
            if (edge == null)
            {                                                                                                                    
                throw new Exception("Edge not found");
            }
            return _mapper.Map<EdgeDTO>(edge);
        }

        public List<EdgeDTO> GetAllEdges()
        {
            try
            {
                var edges = _edgeDAO.GetAllEdges();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
                var edgeDTOs = edges.Select(edge => _mapper.Map<EdgeDTO>(edge)).ToList();
                return edgeDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting all edges.", ex);
            }
        }

        public void AddEdge(EdgeAddDTO edge)
        {
            try
            {
                var edgeModel = new Edge
                {
                    MapPointA = edge.MapPointA,
                    MapPointB = edge.MapPointB,
                    Direction = edge.Direction,
                    Distance = edge.Distance
                };
                _edgeDAO.AddEdge(edgeModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding edge.", ex);
            }
        }

        public void UpdateEdge(EdgeUpdateDTO edge)
        {
            try
            {
                var edgeModel = new Edge
                {
                    EdgeId = edge.EdgeId,
                    MapPointA = edge.MapPointA,
                    MapPointB = edge.MapPointB,
                    Direction = edge.Direction,
                    Distance = edge.Distance
                };
                _edgeDAO.UpdateEdge(_mapper.Map<Edge>(edgeModel));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating edge.", ex);
            }
        }

        public void DeleteEdge(int edgeId)
        {
            try
            {
                _edgeDAO.DeleteEdge(edgeId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting edge.", ex);
            }
        }
    }
}
