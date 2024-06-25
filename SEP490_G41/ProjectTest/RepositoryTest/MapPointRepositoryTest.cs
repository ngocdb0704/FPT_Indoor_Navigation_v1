//using System;
//using System.Collections.Generic;
//using System.Linq;
//using AutoMapper;
//using BusinessObject.Models;
//using DataAccess.DAO;
//using DataAccess.IRepository;
//using DataAccess.IRepository.Repository;
//using Microsoft.Extensions.Configuration;
//using Moq;
//using NetTopologySuite.Geometries;
//using Xunit;

//namespace ProjectTest.RepositoryTest
//{
//    public class MapPointRepositoryTests
//    {
//        private readonly Mock<MappointDAO> _mockMappointDAO;
//        private readonly Mock<IMapper> _mockMapper;
//        private readonly MapPointRepository _mapPointRepository;

//        public MapPointRepositoryTests()
//        {
//            _mockMappointDAO = new Mock<MappointDAO>();
//            _mockMapper = new Mock<IMapper>();
//            _mapPointRepository = new MapPointRepository(_mockMappointDAO.Object, _mockMapper.Object);
//        }

//        [Fact]
//        public void GetMapPointById_WhenMapPointIdIsLessThanOrEqualToZero_ThrowsArgumentException()
//        {
//            // Arrange
//            int invalidMapPointId = -1;

//            // Act & Assert
//            Assert.Throws<ArgumentException>(() => _mapPointRepository.GetMapPointById(invalidMapPointId));
//        }

        

//        [Fact]
//        public void AddMapPoint_WhenMapPointDTOIsNull_ThrowsArgumentNullException()
//        {
//            // Arrange
//            MapPointAddDTO nullMapPoint = null;

//            // Act & Assert
//            Assert.Throws<ArgumentNullException>(() => _mapPointRepository.AddMapPoint(nullMapPoint));
//        }

//        [Fact]
//        public void UpdateMapPoint_WhenMapPointDTOIsNull_ThrowsArgumentNullException()
//        {
//            // Arrange
//            MapPointUpdateDTO nullMapPoint = null;

//            // Act & Assert
//            Assert.Throws<ArgumentNullException>(() => _mapPointRepository.UpdateMapPoint(nullMapPoint));
//        }

//        [Fact]
//        public void UpdateMapPoint_WhenMapPointIdIsLessThanOrEqualToZero_ThrowsArgumentException()
//        {
//            // Arrange
//            MapPointUpdateDTO mapPoint = new MapPointUpdateDTO { MapPointId = -1 };

//            // Act & Assert
//            Assert.Throws<ArgumentException>(() => _mapPointRepository.UpdateMapPoint(mapPoint));
//        }

//        [Fact]
//        public void DeleteMapPoint_WhenMapPointIdIsLessThanOrEqualToZero_ThrowsArgumentException()
//        {
//            // Arrange
//            int invalidMapPointId = -1;

//            // Act & Assert
//            Assert.Throws<ArgumentException>(() => _mapPointRepository.DeleteMapPoint(invalidMapPointId));
//        }
//    }
//}