using BusinessObject.DTO;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DAO
{
    public class MapDAO
    {
        private readonly finsContext _context;

        public MapDAO(finsContext context)
        {
            _context = context;
        }
        public MapDAO()
        {
            _context = new finsContext();
        }


        // Thêm mới bản đồ
        public void AddMap(Map map, int memberID)
        {
            if (map == null)
                throw new ArgumentNullException(nameof(map));

            if (string.IsNullOrWhiteSpace(map.MapName))
                throw new ArgumentException("Map name cannot be null or empty.");

            if (map.MapImage2D == null)
                throw new ArgumentException("2D image cannot be null.");

            if (map.FloorId <= 0)
                throw new ArgumentException("Floor ID must be a positive integer.");

            // Thêm mới bản đồ
            _context.Maps.Add(map);
            _context.SaveChanges();

            // Thêm mới bản đồ quản lý (Mapmanage)
            var mapManage = new Mapmanage
            {
                MapId = map.MapId,
                MemberId = memberID,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            _context.Mapmanages.Add(mapManage);

            // Lưu thay đổi
            _context.SaveChanges();
            _context.Dispose();
        }

        // Đọc thông tin bản đồ bằng Id
        public virtual Map GetMapById(int mapId)
        {
            if (mapId <= 0)
                throw new ArgumentException("Map ID must be a positive integer.");

            var map = _context.Maps.Include(m => m.Floor).FirstOrDefault(m => m.MapId == mapId);
            return map;
        }

        // Cập nhật thông tin bản đồ
        public void UpdateMap(MapUpdateDTO mapDto)
        {
            if (mapDto == null)
                throw new ArgumentNullException(nameof(mapDto));

            if (mapDto.MapId <= 0)
                throw new ArgumentException("Map ID must be a positive integer.");

            if (string.IsNullOrWhiteSpace(mapDto.MapName))
                throw new ArgumentException("Map name cannot be null or empty.");

            if (mapDto.MapImage2D == null)
                throw new ArgumentException("2D image cannot be null.");

            if (mapDto.FloorId <= 0)
                throw new ArgumentException("Floor ID must be a positive integer.");

            try
            {
                var map = _context.Maps.FirstOrDefault(m => m.MapId == mapDto.MapId);
                if (map != null)
                {
                    string uniqueFileName = mapDto.MapImage2D.FileName;

                    map.MapName = mapDto.MapName;
                    map.MapImage2D = uniqueFileName;
                    map.FloorId = mapDto.FloorId;

                    _context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException($"Map with ID {mapDto.MapId} does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating map.", ex);
            }
        }


        // Xóa bản đồ bằng Id
        public string DeleteMap(int mapId)
        {
            if (mapId <= 0)
                throw new ArgumentException("Map ID must be a positive integer.");

            using (var context = new finsContext())
            {
                var map = context.Maps.FirstOrDefault(m => m.MapId == mapId);
                if (map != null)
                {
                    // Xóa thông tin trong bảng Mapmanage
                    var mapManages = context.Mapmanages.Where(mm => mm.MapId == mapId);
                    context.Mapmanages.RemoveRange(mapManages);

                    // Xóa bản đồ từ bảng Maps
                    context.Maps.Remove(map);

                    // Lưu thay đổi
                    context.SaveChanges();
                    _context.Dispose();
                    // Trả về thông báo xóa thành công
                    return $"Map with ID {mapId} has been successfully deleted.";
                }
                else
                {
                    throw new ArgumentException($"Map with ID {mapId} does not exist.");
                }
            }
        }




        // Lấy danh sách tất cả các bản đồ
        public virtual List<Map> GetAllMaps()
        {
            var maps = _context.Maps
       .Include(m => m.Floor)
           .ThenInclude(f => f.Building)
       .Include(m => m.Mapmanages)
           .ThenInclude(mm => mm.Member)
       .Include(m => m.Mappoints)
       .ToList();
            _context.Dispose();
            return maps;
        }

        // Tìm kiếm bản đồ theo tên
        public List<Map> SearchMapsByName(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<Map>();

            var maps = _context.Maps
                .Where(m => m.MapName.Contains(keyword))
                .ToList();
            _context.Dispose();
            return maps;
        }
    }
}