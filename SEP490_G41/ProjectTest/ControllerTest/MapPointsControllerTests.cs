//using AR_NavigationAPI.Controllers;
//using DataAccess.IRepository;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.OData.Results;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ProjectTest.ControllerTest
//{
//    public class MapPointsControllerTests
//    {
//        private readonly Mock<IMapPointRepository> _mockMapPointRepository;
//        private readonly MapPointsController _controller;

//        public MapPointsControllerTests()
//        {
//            _mockMapPointRepository = new Mock<IMapPointRepository>();
//            _controller = new MapPointsController(_mockMapPointRepository.Object);
//        }

//        [Fact]
//        public void GetAllMapPoints_ReturnsOkObjectResult()
//        {
//            // Arrange
//            var expectedMapPoints = new List<MapPointDTO>
//    {
//        new MapPointDTO
//        {
//            MapPointId = 1,
//            MapId = 1,
//            MappointName = "Point 1",
//            LocationWeb = "[10, 20]",
//            LocationApp = "[11, 21]",
//            LocationGps = "[12, 22]",
//            FloorId = 1,
//            BuildingId = 101,
//            Image = "point1.jpg"
//        },
//        new MapPointDTO
//        {
//            MapPointId = 2,
//            MapId = 1,
//            MappointName = "Point 2",
//            LocationWeb = "[30, 40]",
//            LocationApp = "[31, 41]",
//            LocationGps = "[32, 42]",
//            FloorId = 2,
//            BuildingId = 102,
//            Image = "point2.jpg"
//        }
//    };
//            _mockMapPointRepository.Setup(repo => repo.GetAllMapPoints()).Returns(expectedMapPoints);

//            // Act
//            var result = _controller.GetAllMapPoints();

//            // Assert
//            Assert.IsType<OkObjectResult>(result);
//            var okResult = (OkObjectResult)result;
//            Assert.Equal(expectedMapPoints, okResult.Value);
//            Assert.Equal(2, ((List<MapPointDTO>)okResult.Value).Count);
//            Assert.Equal("Point 1", ((List<MapPointDTO>)okResult.Value)[0].MappointName);
//            Assert.Equal("[11, 21]", ((List<MapPointDTO>)okResult.Value)[0].LocationApp);
//            Assert.Equal(1, ((List<MapPointDTO>)okResult.Value)[0].FloorId);
//            Assert.Equal(101, ((List<MapPointDTO>)okResult.Value)[0].BuildingId);
//            Assert.Equal("point1.jpg", ((List<MapPointDTO>)okResult.Value)[0].Image);
//        }

//        [Fact]
//        public void GetMapPointById_WhenIdIsValid_ReturnsOkObjectResult()
//        {
//            // Arrange
//            int validId = 1;
//            var expectedMapPoint = new MapPointDTO
//            {
//                MapPointId = 1,
//                MapId = 1,
//                MappointName = "Point 1",
//                LocationWeb = "[10, 20]",
//                LocationApp = "[11, 21]",
//                LocationGps = "[12, 22]",
//                FloorId = 1,
//                BuildingId = 101,
//                Image = "point1.jpg"
//            };
//            _mockMapPointRepository.Setup(repo => repo.GetMapPointById(validId)).Returns(expectedMapPoint);

//            // Act
//            var result = _controller.GetMapPointById(validId);

//            // Assert
//            Assert.IsType<OkObjectResult>(result);
//            var okResult = (OkObjectResult)result;
//            Assert.Equal(expectedMapPoint, okResult.Value);
//        }


//        [Fact]
//        public void GetMapPointById_WhenMapPointNotFound_ReturnsNotFoundResult()
//        {
//            // Arrange
//            int validId = 1;
//            _mockMapPointRepository.Setup(repo => repo.GetMapPointById(validId)).Returns((MapPointDTO)null);

//            // Act
//            var result = _controller.GetMapPointById(validId);

//            // Assert
//            Assert.IsType<NotFoundResult>(result);
//        }



//        [Fact]
//       /* public void AddMapPoint_WhenMapPointIsValid_ReturnsOkObjectResult()
//        {
//            // Arrange
//            var newMapPoint = new MapPointAddDTO
//            {
//                MapId = 1,
//                MappointName = "Point 1",
//                LocationWeb = "[10, 20]",
//                LocationApp = "[11, 21]",
//                LocationGps = "[12, 22]",
//                FloorId = 1,
//                BuildingId = 101,
//                Image = "point1.jpg"
//            };

//            // Act
//            var result = _controller.AddMapPoint(newMapPoint);

//            // Assert
//            Assert.IsType<OkObjectResult>(result.Result);
//            var okResult = (OkObjectResult)result.Result;
//            Assert.Equal(newMapPoint, okResult.Value);
//        }*/





//        /*  [Fact]
//          public void UpdateMapPointById_WhenMapPointNotFound_ReturnsNotFoundResult()
//          {
//              // Arrange
//              int validId = 1;
//              var mapPoint = new MapPointUpdateDTO {
//                  MapId = 1,
//                  MappointName = "Point 1",
//                  LocationWeb = "[10, 20]",
//                  LocationApp = "[11, 21]",
//                  LocationGps = "[12, 22]",
//                  FloorId = 1,
//                  BuildingId = 101,
//                  Image = "point1.jpg"
//              };
//              _mockMapPointRepository.Setup(repo => repo.GetMapPointById(validId)).Returns((MapPointDTO)null);

//              // Act
//              var result = _controller.UpdateMapPointById(validId, mapPoint);

//              // Assert
//              Assert.IsType<NotFoundResult>(result);
//          }

//          [Fact]
//          public void UpdateMapPointById_WhenMapPointIsValid_ReturnsOkResult()
//          {
//              // Arrange
//              int validId = 1;
//              var mapPoint = new MapPointUpdateDTO { MapPointId = validId,
//                  MapId = 1,
//                  MappointName = "Point 1",
//                  LocationWeb = "[10, 20]",
//                  LocationApp = "[11, 21]",
//                  LocationGps = "[12, 22]",
//                  FloorId = 1,
//                  BuildingId = 101,
//                  Image = "point1.jpg"
//              };
//              var expectedMapPoint = new MapPointDTO { MapPointId = validId,
//                  MapId = 1,
//                  MappointName = "Point 1",
//                  LocationWeb = "[10, 20]",
//                  LocationApp = "[11, 21]",
//                  LocationGps = "[12, 22]",
//                  FloorId = 1,
//                  BuildingId = 101,
//                  Image = "point1.jpg"
//              };
//              _mockMapPointRepository.Setup(repo => repo.GetMapPointById(validId)).Returns(expectedMapPoint);

//              // Act
//              var result = _controller.UpdateMapPointById(validId, mapPoint);

//              // Assert
//              Assert.IsType<OkResult>(result);
//          }



//          [Fact]
//          public void DeleteMapPointById_WhenMapPointNotFound_ReturnsNotFoundResult()
//          {
//              // Arrange
//              int validId = 1;
//              _mockMapPointRepository.Setup(repo => repo.GetMapPointById(validId)).Returns((MapPointDTO)null);

//              // Act
//              var result = _controller.DeleteMapPointById(validId);

//              // Assert
//              Assert.IsType<NotFoundResult>(result);
//          }

//          [Fact]
//          public void DeleteMapPointById_WhenMapPointIsValid_ReturnsOkResult()
//          {
//              // Arrange
//              int validId = 1;
//              var expectedMapPoint = new MapPointDTO { MapPointId = validId,
//                  MapId = 1,
//                  MappointName = "Point 1",
//                  LocationWeb = "[10, 20]",
//                  LocationApp = "[11, 21]",
//                  LocationGps = "[12, 22]",
//                  FloorId = 1,
//                  BuildingId = 101,
//                  Image = "point1.jpg"
//              };
//              _mockMapPointRepository.Setup(repo => repo.GetMapPointById(validId)).Returns(expectedMapPoint);

//              // Act
//              var result = _controller.DeleteMapPointById(validId);

//              // Assert
//              Assert.IsType<OkResult>(result);
//          }
//      }*/
////    }
////}

