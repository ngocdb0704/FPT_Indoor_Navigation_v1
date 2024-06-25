using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DAO
{
    public class MappointDAO
    {
        private readonly finsContext _context;


        public MappointDAO(finsContext context)
        {
            _context = context;
        }

        public MappointDAO()
        {
            _context = new finsContext();
        }

        // Thêm mới mappoint
        public void AddMappoint(Mappoint mappoint)
        {
            if (mappoint == null)
                throw new ArgumentNullException(nameof(mappoint));

            if (mappoint.MapId <= 0)
                throw new ArgumentException("Map ID must be a positive integer.");

            if (mappoint.LocationWeb == null)
                throw new ArgumentException("LocationWeb cannot be null.");

            if (mappoint.LocationApp == null)
                throw new ArgumentException("LocationWeb cannot be null.");

            _context.Mappoints.Add(mappoint);
            _context.SaveChanges();
            _context.Dispose();
        }

        // Đọc thông tin mappoint bằng Id
        public Mappoint GetMappointById(int mappointId)
        {
            if (mappointId <= 0)
                throw new ArgumentException("Mappoint ID must be a positive integer.");

            var mappoint = _context.Mappoints.Include(mp => mp.Map).Include(mp => mp.Building).Include(mp => mp.Floor).FirstOrDefault(mp => mp.MapPointId == mappointId);
            _context.Dispose();
			return mappoint;
        }
		// Cập nhật thông tin mappoint
		public void UpdateMappoint(MapPointUpdateDTO dto)
		{
			var mapPointId = dto.MapPointId;
			if (mapPointId <= 0)
			{
				throw new ArgumentException("Invalid map point ID", nameof(mapPointId));
			}

			var mapPoint = _context.Mappoints.FirstOrDefault(mp => mp.MapPointId == mapPointId);
			if (mapPoint == null)
			{
				throw new ArgumentException("This map point does not exist");
			}

			string uniqueFileName = dto.Image?.FileName;

			mapPoint.MapPointId = dto.MapPointId;
			mapPoint.MapPointName = dto.MappointName;


			string[] coordinates = dto.LocationWeb.Trim('[', ']').Split(',');
			double latitude = double.Parse(coordinates[0].Trim());
			double longitude = double.Parse(coordinates[1].Trim());
			mapPoint.LocationWeb = new NetTopologySuite.Geometries.Point (latitude, longitude);

			string[] coordinates1 = dto.LocationWeb.Trim('[', ']').Split(',');
			double latitude1 = double.Parse(coordinates1[0].Trim());
			double longitude1 = double.Parse(coordinates1[1].Trim());
			mapPoint.LocationApp = new NetTopologySuite.Geometries.Point(latitude1, longitude1);

			string[] coordinates2 = dto.LocationWeb.Trim('[', ']').Split(',');
			double latitude2 = double.Parse(coordinates2[0].Trim());
			double longitude2 = double.Parse(coordinates2[1].Trim());
			mapPoint.LocationGps = new NetTopologySuite.Geometries.Point(latitude2, longitude2);

			mapPoint.FloorId = dto.FloorId;
			mapPoint.BuildingId = dto.BuildingId;
			mapPoint.Image = uniqueFileName;
			mapPoint.MapId = dto.MapId;

			_context.Update(mapPoint);
			_context.SaveChanges();

		}

		// Xóa mappoint bằng Id
		public void DeleteMappoint(int mappointId)
        {
            if (mappointId <= 0)
                throw new ArgumentException("Mappoint ID must be a positive integer.");

            var mappoint = _context.Mappoints
                .Include(m => m.Mappointices) // Include related Mappointex entries
                .Include(m => m.Mappointroutes) // Include related Mappointroute entries
                .SingleOrDefault(m => m.MapPointId == mappointId);

            if (mappoint != null)
            {
                // Remove edges associated with the map point
                var edgesToRemove = _context.Edges.Where(e => e.MapPointA == mappointId || e.MapPointB == mappointId);
                _context.Edges.RemoveRange(edgesToRemove);

                // Remove related Mappointex entries
                _context.Mappointices.RemoveRange(mappoint.Mappointices);

                // Remove related Mappointroute entries
                _context.Mappointroutes.RemoveRange(mappoint.Mappointroutes);

                // Remove the map point itself
                _context.Mappoints.Remove(mappoint);
                _context.SaveChanges();
                _context.Dispose();
            }
            else
            {
                throw new ArgumentException($"Mappoint with ID {mappointId} does not exist.");
            }
        }


        // Lấy tất cả các mappoint
        public List<Mappoint> GetAllMappoints()
        {
            try
            {
                var mappoints = _context.Mappoints.Include(x=>x.Building).Include(x => x.Floor).ToList();
                _context.Dispose();
                return mappoints;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting all map points.", ex);
            }
        }
    }
}
