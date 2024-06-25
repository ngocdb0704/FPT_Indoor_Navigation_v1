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
    public class FacilityRepository : IFacilityRepository
    {
        private readonly FacilityDAO _facilityDAO;
        private readonly IMapper _mapper;

        public FacilityRepository(FacilityDAO facilityDAO, IMapper mapper)
        {
            _facilityDAO = facilityDAO;
            _mapper = mapper;
        }

        public FacilityDTO GetFacilityById(int facilityId)
        {
            try
            {
                return _mapper.Map<FacilityDTO>(_facilityDAO.GetFacilityById(facilityId));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting facility by ID.", ex);
            }
        }

        public List<FacilityDTO> GetAllFacilities()
        {
            try
            {
                var facilities = _facilityDAO.GetAllFacilities();
                var facilityDTOs = facilities.Select(facility => _mapper.Map<FacilityDTO>(facility)).ToList();
                return facilityDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting all facilities.", ex);
            }
        }

        public void AddFacility(FacilityAddDTO facility)
        {
            try
            {
                _facilityDAO.AddFacility(_mapper.Map<Facility>(facility));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding facility.", ex);
            }
        }

     /*   public void UpdateFacility(FacilityUpdateDTO facility)
        {
            try
            {
                _facilityDAO.UpdateFacility(_mapper.Map<Facility>(facility));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating facility.", ex);
            }
        }

        public void DeleteFacility(int facilityId)
        {
            try
            {
                _facilityDAO.DeleteFacility(facilityId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting facility.", ex);
            }
        }*/
    }
}
