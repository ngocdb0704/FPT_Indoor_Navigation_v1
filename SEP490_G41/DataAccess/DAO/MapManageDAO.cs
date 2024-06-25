using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class MapManageDAO
    {
        private readonly finsContext _context;

        public MapManageDAO(finsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddMapManage(Mapmanage mapManage)
        {
            if (mapManage == null)
            {
                throw new ArgumentNullException(nameof(mapManage));
            }

            _context.Mapmanages.Add(mapManage);
            _context.SaveChanges();
            _context.Dispose();
        }

        public Mapmanage GetMapManageById(int mapId)
        {
            if (mapId <= 0)
            {
                throw new ArgumentException("Invalid map ID", nameof(mapId));
            }

            var mapmange = _context.Mapmanages.FirstOrDefault(m => m.MapId == mapId);
            return mapmange;
        }

        public void UpdateMapManage(Mapmanage mapManage)
        {
            if (mapManage == null)
            {
                throw new ArgumentNullException(nameof(mapManage));
            }

            var existingMapManage = _context.Mapmanages.FirstOrDefault(m => m.MapId == mapManage.MapId);

            if (existingMapManage != null)
            {
                existingMapManage.MemberId = mapManage.MemberId;
                existingMapManage.CreateDate = mapManage.CreateDate;
                existingMapManage.UpdateDate = mapManage.UpdateDate;

                _context.SaveChanges();
                _context.Dispose();
            }
        }

        public void DeleteMapManage(int mapId)
        {
            if (mapId <= 0)
            {
                throw new ArgumentException("Invalid map ID", nameof(mapId));
            }

            var mapManage = _context.Mapmanages.FirstOrDefault(m => m.MapId == mapId);
            if (mapManage != null)
            {
                _context.Mapmanages.Remove(mapManage);
                _context.SaveChanges();
                _context.Dispose();
            }
        }

        public List<Mapmanage> GetAllMapManages()
        {
            var mapmanage = _context.Mapmanages.ToList();
            _context.Dispose(); 
            return mapmanage;
        }
    }
}
