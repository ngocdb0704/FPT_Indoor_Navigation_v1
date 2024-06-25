using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository.Repository;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ProjectTest.RepositoryTest
{
    public class MapRepositoryTests
    {
        private readonly Mock<MapDAO> _mockMapDAO;
        private readonly Mock<IMapper> _mockMapper;
        private readonly MapRepository _mapRepository;

        public MapRepositoryTests()
        {
            _mockMapDAO = new Mock<MapDAO>();
            _mockMapper = new Mock<IMapper>();
            _mapRepository = new MapRepository(_mockMapDAO.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetMapById_WhenMapIdIsLessThanOrEqualToZero_ThrowsArgumentException()
        {
            // Arrange
            int invalidMapId = -1;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mapRepository.GetMapById(invalidMapId));
        }

        [Fact]
        public void GetMapById_WhenMapNotFound_ThrowsException()
        {
            // Arrange
            int validMapId = 1;
            _mockMapDAO.Setup(m => m.GetMapById(validMapId)).Returns((Map)null);

            // Act & Assert
            Assert.Throws<Exception>(() => _mapRepository.GetMapById(validMapId));
        }

        [Fact]
        public void GetAllMaps_ReturnsListOfMapDTOs()
        {
            // Arrange
            var mockMaps = new List<Map>
    {
        new Map { MapId = 1, MapName = "Map 1" },
        new Map { MapId = 2, MapName = "Map 2" }
    };

            _mockMapDAO.Setup(m => m.GetAllMaps()).Returns(mockMaps);
            _mockMapper.Setup(m => m.Map<MapDTO>(It.IsAny<Map>()))
                .Returns((Map map) => new MapDTO { MapId = map.MapId, MapName = map.MapName });

            // Act
            var result = _mapRepository.GetAllMaps();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].MapId);
            Assert.Equal("Map 1", result[0].MapName);
            Assert.Equal(2, result[1].MapId);
            Assert.Equal("Map 2", result[1].MapName);
        }

        //[Fact]
        //public void AddMap_WhenMapDTOIsNull_ThrowsArgumentNullException()
        //{
        //    // Arrange
        //    MapAddDTO nullMap = null;

        //    // Act & Assert
        //    Assert.Throws<ArgumentNullException>(() => _mapRepository.AddMap(nullMap));
        //}

        [Fact]
        public void UpdateMap_WhenMapDTOIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            MapUpdateDTO nullMap = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _mapRepository.UpdateMap(nullMap));
        }

        [Fact]
        public void UpdateMap_WhenMapIdIsLessThanOrEqualToZero_ThrowsArgumentException()
        {
            // Arrange
            MapUpdateDTO map = new MapUpdateDTO { MapId = -1 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mapRepository.UpdateMap(map));
        }

        [Fact]
        public void DeleteMap_WhenMapIdIsLessThanOrEqualToZero_ThrowsArgumentException()
        {
            // Arrange
            int invalidMapId = -1;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mapRepository.DeleteMap(invalidMapId));
        }
    }
}
