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
    public class PathShortest
    {
        private readonly EdgeDAO _edgeDAO;
        private readonly MappointDAO _mappointDAO;
        private readonly finsContext _context;
        public PathShortest(MappointDAO mappointDAO, EdgeDAO edgeDAO, finsContext _context)
        {
            _mappointDAO = mappointDAO;
            _edgeDAO = edgeDAO;
        }

        public Mappoint FindPoint(int input)
        {
            using (var context = new finsContext())
            {
                List<Mappoint> listOfMapPoint = _mappointDAO.GetAllMappoints();
                foreach (Mappoint find in listOfMapPoint)
                {
                    if (find.MapPointId.Equals(input))
                    {
                        return find;
                    }
                }
                return null;
            }
        }

        // Có thể thêm phương thức để truy vấn một MapPoint theo Id mà không Dispose
        public Mappoint GetMappointById(int id)
        {
            using (var context = new finsContext())
            {
                // Thực hiện truy vấn để lấy Mappoint theo ID từ context tạm thời
                return context.Mappoints.FirstOrDefault(mp => mp.MapPointId == id);
            }
        }

        public double NumberFomula(double ax, double ay, double bx, double by)
        {
            double result;
            double half1, half2;
            half1 = ax - bx;
            half2 = ay - by;
            result = half1 * half1 + half2 * half2;
            return result;
        }

        public List<Mappoint> PointInArea(Mappoint pos1, Mappoint pos2, int multi)
        {
            using (var context = new finsContext())
            {
                List<Mappoint> listOfMapPoint = _mappointDAO.GetAllMappoints();
                List<Mappoint> pointInAreaList = new List<Mappoint>();
                double result;
                double rotate = NumberFomula(pos2.LocationApp.X,
                    pos2.LocationApp.Y, pos1.LocationApp.X,
                    pos1.LocationApp.Y);
                foreach (Mappoint find in listOfMapPoint)
                {
                    result = NumberFomula(find.LocationApp.X,
                        find.LocationApp.Y, pos1.LocationApp.X,
                        pos1.LocationApp.Y);
                    if (result <= rotate * multi && (find.FloorId == pos1.FloorId || find.FloorId == pos2.FloorId))
                    {
                        if (!pointInAreaList.Contains(find))
                        {
                            pointInAreaList.Add(find);
                        }
                    }
                }
                return pointInAreaList;
            }
        }

        public List<Mappoint> GetAdjacentMapPoint(int mapPointId, List<Mappoint> pointInAreaList)
        {
            using (var context = new finsContext())
            {
                List<Mappoint> adjacentMapPoints = new List<Mappoint>();
                // Truy vấn danh sách các cạnh trực tiếp từ DbContext
                var edges = context.Edges.ToList();
                // Duyệt qua danh sách các cạnh
                foreach (Edge edge in edges)
                {
                    // Kiểm tra nếu mapPointId hiện tại nằm trong cạnh này
                    if (edge.MapPointA == mapPointId || edge.MapPointB == mapPointId)
                    {
                        // Lấy MapPointId của điểm lân cận
                        int adjacentMapPointId = edge.MapPointA == mapPointId ? edge.MapPointB : edge.MapPointA;

                        // Tìm đối tượng Mappoint tương ứng trong danh sách pointInAreaList
                        Mappoint adjacentMapPoint = pointInAreaList.Find(mp => mp.MapPointId == adjacentMapPointId);

                        // Nếu tìm thấy, thêm vào danh sách adjacentMapPoints
                        if (adjacentMapPoint != null)
                        {
                            adjacentMapPoints.Add(adjacentMapPoint);
                        }
                    }
                }
                return adjacentMapPoints;
            }
        }

        public int getIndex(int mappointId, List<Mappoint> pointInAreaList)
        {
            using (var context = new finsContext())
            {
                int index = 0;
                while (pointInAreaList.Count() > index)
                {
                    if (mappointId == pointInAreaList[index].MapPointId)
                    {
                        return index;
                    }
                    index++;
                }
                return -1;
            }
        }

        public int FindMinDistance(double[] shortestDistances, bool[] visited)
        {
            using (var context = new finsContext())
            {
                double minDistance = double.MaxValue;
                int minMapPoint = -1;
                for (int mapPoint = 0; mapPoint < shortestDistances.Length; mapPoint++)
                {
                    if (!visited[mapPoint] && shortestDistances[mapPoint] <= minDistance)
                    {
                        minDistance = shortestDistances[mapPoint];
                        minMapPoint = mapPoint;
                    }
                }
                return minMapPoint;
            }
        }

        public Edge GetEdge(int p1, int p2)
        {
            using (var context = new finsContext())
            {
                try
                {
                    var edge = context.Edges.FirstOrDefault(e =>
                        (e.MapPointA == p1 && e.MapPointB == p2) || (e.MapPointA == p2 && e.MapPointB == p1));

                    return edge;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while getting edge.", ex);
                }
            }
        }

        public List<Mappoint> Dijkstra(int inputPosition, int inputDestination, int multi)
        {
            using (var context = new finsContext())
            {
                List<Mappoint> pointInAreaList = PointInArea(GetMappointById(inputPosition), GetMappointById(inputDestination), multi);
                int position = getIndex(inputPosition, pointInAreaList);
                int destination = getIndex(inputDestination, pointInAreaList);
                int numMapPoint = pointInAreaList.Count;
                double[] shortestDistances = new double[numMapPoint];
                int[] previousMapPoint = new int[numMapPoint];
                bool[] visited = new bool[numMapPoint];
                int currentMapPoint;
                Array.Fill(shortestDistances, double.MaxValue);
                Array.Fill(previousMapPoint, -1);
                shortestDistances[position] = 0;

                for (int count = 0; count < numMapPoint - 1; count++)
                {
                    currentMapPoint = FindMinDistance(shortestDistances, visited);
                    visited[currentMapPoint] = true;

                    if (currentMapPoint == destination)
                    {
                        break;
                    }
                    int currentMapPointName = pointInAreaList[currentMapPoint].MapPointId;
                    List<Mappoint> adjacentMapPoints = GetAdjacentMapPoint(currentMapPointName, pointInAreaList);
                    foreach (Mappoint mapPoint in adjacentMapPoints)
                    {
                        int mapPointIndex = getIndex(mapPoint.MapPointId, pointInAreaList);
                        double edgeWeight = GetEdge(currentMapPointName, mapPoint.MapPointId).Distance;
                        if (!visited[mapPointIndex] && edgeWeight != -1.0
                                && shortestDistances[currentMapPoint] != double.MaxValue
                                && shortestDistances[currentMapPoint] + edgeWeight < shortestDistances[mapPointIndex])
                        {
                            shortestDistances[mapPointIndex] = shortestDistances[currentMapPoint] + edgeWeight;
                            previousMapPoint[mapPointIndex] = currentMapPoint;
                        }
                    }
                }

                List<Mappoint> ans = new List<Mappoint>();
                currentMapPoint = destination;
                while (currentMapPoint != -1)
                {
                    ans.Insert(0, pointInAreaList[currentMapPoint]);
                    currentMapPoint = previousMapPoint[currentMapPoint];
                }
                return ans;
            }
        }
    }
}
