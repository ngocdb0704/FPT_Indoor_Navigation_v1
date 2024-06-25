using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json.Linq;


namespace DataAccess.IRepository.Repository
{
    public class MapPointRepository : IMapPointRepository
    {
        private readonly MappointDAO _mappointDAO;
        private readonly IMapper _mapper;
        private readonly FloorDAO _floorDAO;
        private readonly BuildingDAO _buildingDAO;
        private readonly PathShortest _pathShortest;


        public MapPointRepository(MappointDAO mappointDAO,BuildingDAO buildingDAO, FloorDAO floorDAO, PathShortest pathShortest, IMapper mapper)
        {
            _mappointDAO = mappointDAO;
            _buildingDAO = buildingDAO;
            _floorDAO = floorDAO;
            _mapper = mapper;
            _pathShortest = pathShortest;

        }
		public MapPointDTO GetMapPointById(int mapPointId)
		{
			var mapPoint = _mappointDAO.GetMappointById(mapPointId);

			if (mapPoint == null)
			{
				throw new Exception("MapPoint not found");
			}

			return new MapPointDTO
			{
				MapPointId = mapPoint.MapPointId,
				MapId = mapPoint.MapId,
				MappointName = mapPoint.MapPointName,
				LocationWeb = FormatCoordinates(mapPoint.LocationWeb),
				LocationApp = FormatCoordinates(mapPoint.LocationApp),
				LocationGps = mapPoint.LocationGps == null ? null : FormatCoordinates(mapPoint.LocationGps),
				FloorId = mapPoint.FloorId,
				BuildingId = mapPoint.BuildingId,
				Image = mapPoint.Image,
				Destination = mapPoint.Destination ?? false,
				BuildingName = mapPoint.Building?.BuildingName,
				FloorName = mapPoint.Floor?.FloorName
			};
		}

		private string FormatCoordinates(Point point)
        {
            if (point == null)
                return null;

            var x = point.X.ToString("0.00");
            var y = point.Y.ToString("0.00");

            return $"[{x},{y}]";
        }

        public List<MapPointDTO> GetAllMapPoints()
        {
            try
            {
                var mapPoints = _mappointDAO.GetAllMappoints();
                  //  .Join(_buildingDAO.GetAllBuildings(), mp => mp.BuildingId, b => b.BuildingId, (mp, b) => new { MapPoint = mp, Building = b })
                  //  .Join(_floorDAO.GetAllFloors(), mpb => mpb.MapPoint.FloorId, f => f.FloorId, (mpb, f) => new { mpb.MapPoint, mpb.Building, Floor = f });

                var mapPointDTOs = mapPoints
                    .Select(mpbf => new MapPointDTO
                    {
                        MapPointId = mpbf.MapPointId,
                        MapId = mpbf.MapId,
                        MappointName = mpbf.MapPointName,
                        LocationWeb = ExtractCoordinatesFromGeoJson(ConvertPointToGeoJson(mpbf.LocationWeb)),
                        LocationGps = ExtractCoordinatesFromGeoJson(ConvertPointToGeoJson(mpbf.LocationGps ?? GeometryFactory.Default.CreatePoint(new Coordinate(0, 0)))),
                        LocationApp = ExtractCoordinatesFromGeoJson(ConvertPointToGeoJson(mpbf.LocationApp)),
                        FloorId = mpbf.FloorId,
                        BuildingId = mpbf.BuildingId,
                        Image = mpbf.Image,
                        Destination = mpbf.Destination ?? false,
                        BuildingName = mpbf.Building.BuildingName,
                        FloorName = mpbf.Floor.FloorName
                    })
                    .ToList();

                return mapPointDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting all map points.", ex);
            }
        }
		public List<MapPointDTO> GetAllMapPointsPath(int inputPosition, int inputDestination)
		{
			try
			{
				int multi = 1;
				var mapPoints = _pathShortest.Dijkstra(inputPosition, inputDestination, multi)
					.Join(GetAllBuildings(), mp => mp.BuildingId, b => b.BuildingId, (mp, b) => new { MapPoint = mp, Building = b })
					.Join(GetAllFloors(), mpb => mpb.MapPoint.FloorId, f => f.FloorId, (mpb, f) => new { mpb.MapPoint, mpb.Building, Floor = f });

				while (mapPoints.Count() == 1)
				{
					multi++;
					mapPoints = _pathShortest.Dijkstra(inputPosition, inputDestination, multi)
						.Join(GetAllBuildings(), mp => mp.BuildingId, b => b.BuildingId, (mp, b) => new { MapPoint = mp, Building = b })
						.Join(GetAllFloors(), mpb => mpb.MapPoint.FloorId, f => f.FloorId, (mpb, f) => new { mpb.MapPoint, mpb.Building, Floor = f });
				}

				var mapPointDTOs = mapPoints
					.Select(mpbf => new MapPointDTO
					{
						MapPointId = mpbf.MapPoint.MapPointId,
						MapId = mpbf.MapPoint.MapId,
						MappointName = mpbf.MapPoint.MapPointName,
						LocationWeb = ExtractCoordinatesFromGeoJson(ConvertPointToGeoJson(mpbf.MapPoint.LocationWeb)),
						LocationGps = ExtractCoordinatesFromGeoJson(ConvertPointToGeoJson(mpbf.MapPoint.LocationGps ?? GeometryFactory.Default.CreatePoint(new Coordinate(0, 0)))),
						LocationApp = ExtractCoordinatesFromGeoJson(ConvertPointToGeoJson(mpbf.MapPoint.LocationApp)),
						FloorId = mpbf.MapPoint.FloorId,
						BuildingId = mpbf.MapPoint.BuildingId,
						Image = mpbf.MapPoint.Image,
						Destination = mpbf.MapPoint.Destination ?? false,
						BuildingName = mpbf.Building.BuildingName,
						FloorName = mpbf.Floor.FloorName
					})
					.ToList();

				return mapPointDTOs;
			}
			catch (Exception ex)
			{
				throw new Exception("Error occurred while getting all map points.", ex);
			}
		}

        // Định nghĩa phương thức để truy vấn tất cả các tòa nhà và tầng mà không dispose
        private IEnumerable<Building> GetAllBuildings()
        {
            using (var context = new finsContext())
            {
                // Thực hiện truy vấn tất cả các tòa nhà
                return context.Buildings.ToList(); // Trả về danh sách tất cả các tòa nhà từ context tạm thời
            }
        }

        private IEnumerable<Floor> GetAllFloors()
        {
            using (var context = new finsContext())
            {
                // Thực hiện truy vấn tất cả các tầng
                return context.Floors.ToList(); // Trả về danh sách tất cả các tầng từ context tạm thời
            }
        }

        private string ConvertPointToGeoJson(Point point)
        {
            var feature = new NetTopologySuite.Features.Feature(point, new AttributesTable());
            var writer = new GeoJsonWriter();
            return writer.Write(feature);
        }
        private string ExtractCoordinatesFromGeoJson(string geoJson)
        {
            // Kiểm tra xem đầu vào có phải là null hay không
            if (string.IsNullOrEmpty(geoJson))
            {
                return null;
            }

            try
            {
                // Parse chuỗi JSON thành đối tượng JObject
                var jObject = JObject.Parse(geoJson);

                // Trích xuất giá trị của trường 'coordinates' và chuyển đổi thành chuỗi
                string coordinatesJson = jObject["geometry"]["coordinates"].ToString();

                // Loại bỏ các ký tự xuống dòng và khoảng trắng
                coordinatesJson = coordinatesJson.Replace("\r\n", "").Replace(" ", "");

                return coordinatesJson;
            }
            catch (Exception)
            {
                // Nếu có bất kỳ lỗi nào xảy ra trong quá trình xử lý, trả về null
                return null;
            }
        }
        public void AddMapPoint(MapPointAddDTO mapPoint)
        {
            if (mapPoint == null)
            {
                throw new ArgumentNullException(nameof(mapPoint), "MapPoint DTO cannot be null.");
            }

            try
            {
                // Parse the location string to extract the coordinates
                string[] coordinates = mapPoint.LocationWeb.Trim('[', ']').Split(',');
                double latitude = double.Parse(coordinates[0].Trim());
                double longitude = double.Parse(coordinates[1].Trim());

                string[] coordinates1 = mapPoint.LocationApp.Trim('[', ']').Split(',');
                double latitude1 = double.Parse(coordinates1[0].Trim());
                double longitude1 = double.Parse(coordinates1[1].Trim());

                string[] coordinates2 = mapPoint.LocationGps.Trim('[', ']').Split(',');
                double latitude2 = double.Parse(coordinates2[0].Trim());
                double longitude2 = double.Parse(coordinates2[1].Trim());

                // Create a new Point object and map the DTO to the entity
                Point location = new Point(longitude, latitude);
                var mapPointEntity = _mapper.Map<Mappoint>(mapPoint);

                Point location1 = new Point(longitude, latitude);
                var mapPointEntity1 = _mapper.Map<Mappoint>(mapPoint);

                Point location2 = new Point(longitude, latitude);
                var mapPointEntity2 = _mapper.Map<Mappoint>(mapPoint);


                mapPointEntity.LocationWeb = location;
                mapPointEntity.LocationApp = location1;
                mapPointEntity.LocationGps = location2;

                _mappointDAO.AddMappoint(mapPointEntity);
            }
            catch (MySqlConnector.MySqlException ex)
            {
                throw new Exception($"Error occurred while adding map point. Message: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding map point.", ex);
            }
        }

        public void UpdateMapPoint(MapPointUpdateDTO mapPoint)
        {
            if (mapPoint == null)
            {
                throw new ArgumentNullException(nameof(mapPoint), "MapPointUpdateDTO cannot be null.");
            }

            if (mapPoint.MapPointId <= 0)
            {
                throw new ArgumentException("Mappoint ID must be a positive integer.");
            }

            try
            {
                _mappointDAO.UpdateMappoint(mapPoint);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating map point.", ex);
            }
        }
        public void DeleteMapPoint(int mapPointId)
        {
            if (mapPointId <= 0)
            {
                throw new ArgumentException("Mappoint ID must be a positive integer.");
            }

            try
            {
                _mappointDAO.DeleteMappoint(mapPointId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting map point.", ex);
            }
        }
    }
}
