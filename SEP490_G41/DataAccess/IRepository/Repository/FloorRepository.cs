using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository.Repository
{
    public class FloorRepository : IFloorRepository
    {
        private readonly FloorDAO _floorDAO;
        private readonly IMapper _mapper;

        public FloorRepository(FloorDAO floorDAO, IMapper mapper)
        {
            _floorDAO = floorDAO;
            _mapper = mapper;
        }

        public FloorDTO GetFloorById(int floorId)
        {
            try
            {
                return _mapper.Map<FloorDTO>(_floorDAO.GetFloorById(floorId));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting floor by ID.", ex);
            }
        }

        public List<FloorDTO> GetAllFloors()
        {
            try
            {
                var floors = _floorDAO.GetAllFloors();
                var floorDTOs = floors.Select(floor => _mapper.Map<FloorDTO>(floor)).ToList();
                return floorDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting all floors.", ex);
            }
        }
        public List<FloorDTO> GetAllFloorsByBuildingId(int buildingId)
        {
            try
            {
                var floors = _floorDAO.GetAllFloorsByBuildingId(buildingId);
                var floorDTOs = floors.Select(floor => _mapper.Map<FloorDTO>(floor)).ToList();
                return floorDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while getting all floors by building ID: {buildingId}", ex);
            }
        }

        public void AddFloor(FloorAddDTO floor)
        {
            try
            {
                _floorDAO.AddFloor(_mapper.Map<Floor>(floor));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding floor.", ex);
            }
        }

       /* public void UpdateFloor(FloorDTO floor)
        {
            try
            {
                _floorDAO.UpdateFloor(_mapper.Map<Floor>(floor));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating floor.", ex);
            }
        }

        public void DeleteFloor(int floorId)
        {
            try
            {
                _floorDAO.DeleteFloor(floorId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting floor.", ex);
            }
        }*/
    }
}
