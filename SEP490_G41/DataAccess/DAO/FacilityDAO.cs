using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class FacilityDAO
    {
        private readonly finsContext _context;

        public FacilityDAO(finsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Thêm mới tòa nhà
        public void AddFacility(Facility facility)
        {
            if (facility == null)
            {
                throw new ArgumentNullException(nameof(facility));
            }

            _context.Facilities.Add(facility);
            _context.SaveChanges();
            _context.Dispose();
        }

        // Đọc thông tin tòa nhà bằng Id
        public Facility GetFacilityById(int facilityId)
        {
            if (facilityId <= 0)
            {
                throw new ArgumentException("Invalid facility ID", nameof(facilityId));
            }

            var facility = _context.Facilities.FirstOrDefault(f => f.FacilityId == facilityId);
            _context.Dispose();
            return facility;
        }

       
/*
        public void UpdateFacility(Facility facility)
        {
            if (facility == null)
            {
                throw new ArgumentNullException(nameof(facility));
            }

            var existingFacility = _context.Facilities.FirstOrDefault(f => f.FacilityId == facility.FacilityId);
            if (existingFacility == null)
            {
                throw new ArgumentException("Facility not found", nameof(facility));
            }

            existingFacility.FacilityName = facility.FacilityName;
            existingFacility.Address = facility.Address;
            existingFacility.Status = facility.Status;

            _context.SaveChanges();
            _context.Dispose();
        }

        public void DeleteFacility(int facilityId)
        {
            if (facilityId <= 0)
            {
                throw new ArgumentException("Invalid facility ID", nameof(facilityId));
            }

            var facility = _context.Facilities.FirstOrDefault(f => f.FacilityId == facilityId);
            if (facility == null)
            {
                throw new ArgumentException("Facility not found", nameof(facilityId));
            }

            _context.Facilities.Remove(facility);
            _context.SaveChanges();
            _context.Dispose();
        }*/

        public List<Facility> GetAllFacilities()
        {
            var facilities = _context.Facilities.ToList();
            _context.Dispose(); return facilities;
        }

    }
}
