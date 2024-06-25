using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IFacilityRepository
    {
        FacilityDTO GetFacilityById(int facilityId);
        List<FacilityDTO> GetAllFacilities();
        void AddFacility(FacilityAddDTO facility);
       /* void UpdateFacility(FacilityUpdateDTO facility);
        void DeleteFacility(int facilityId);*/

    }
}
