using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IEdgeRepository
    {
        EdgeDTO GetEdgeById(int edgeId);
        List<EdgeDTO> GetAllEdges();
        void AddEdge(EdgeAddDTO edge);
        void UpdateEdge(EdgeUpdateDTO edge);
        void DeleteEdge(int edgeId);
    }
}
