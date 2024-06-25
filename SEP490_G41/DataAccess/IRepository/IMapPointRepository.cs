using BusinessObject.DTO;
using DataAccess.DAO;
using System.Collections.Generic;

namespace DataAccess.IRepository
{
    public interface IMapPointRepository
    {
        MapPointDTO GetMapPointById(int mapPointId);
        List<MapPointDTO> GetAllMapPoints();
        List<MapPointDTO> GetAllMapPointsPath(int inputPosition, int inputDestination);
        void AddMapPoint(MapPointAddDTO mapPoint);
        void UpdateMapPoint(MapPointUpdateDTO mapPoint);
        void DeleteMapPoint(int mapPointId);

    }
}
