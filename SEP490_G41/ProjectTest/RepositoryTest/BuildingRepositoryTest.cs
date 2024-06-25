using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;
using DataAccess.IRepository.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTest.RepositoryTest
{
    public class BuildingRepositoryTests
    {

        private Mock<IBuildingRepository> _mockBuildingRepository;
        private IMapper _mapper;

        public BuildingRepositoryTests()
        {
            _mockBuildingRepository = new Mock<IBuildingRepository>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Building, BuildingDTO>();
                // Add other mappings if needed
            }).CreateMapper();
        }

        [Fact]
        public void GetBuildingById_BuildingExists_ReturnsBuildingDTO()
        {
            // Arrange
            int buildingId = 1;
            var buildingDTO = new BuildingDTO { BuildingId = buildingId, BuildingName = "Building 1" };

            _mockBuildingRepository.Setup(repo => repo.GetBuildingById(buildingId)).Returns(buildingDTO);

            // Act
            var result = _mockBuildingRepository.Object.GetBuildingById(buildingId);

            // Assert
            Assert.Equal(buildingDTO, result);
        }

        [Fact]
        public void GetAllBuildings_ReturnsListOfBuildingDTOs()
        {
            // Arrange
            var buildings = new List<BuildingDTO>
            {
                new BuildingDTO { BuildingId = 1, BuildingName = "Building 1" },
                new BuildingDTO { BuildingId = 2, BuildingName = "Building 2" }
            };

            _mockBuildingRepository.Setup(repo => repo.GetAllBuildings()).Returns(buildings);

            // Act
            var result = _mockBuildingRepository.Object.GetAllBuildings();

            // Assert
            Assert.Equal(buildings.Count, result.Count);
            Assert.All(result, b => Assert.Contains(b, buildings));
        }

        [Fact]
        public void AddBuilding_ValidBuilding_AddsBuilding()
        {
            // Arrange
            var buildingToAdd = new BuildingAddDTO
            {
                BuildingName = "New Building",
                Status = "Active",
                FacilityId = 1 // Assuming FacilityId is valid
            };

            // Act
            _mockBuildingRepository.Object.AddBuilding(buildingToAdd);

            // Assert
            _mockBuildingRepository.Verify(repo => repo.AddBuilding(It.IsAny<BuildingAddDTO>()), Times.Once);
        }

        [Fact]
        public void UpdateBuilding_ValidBuilding_UpdatesBuilding()
        {
            // Arrange
            int buildingId = 1;
            var buildingToUpdate = new BuildingUpdateDTO
            {
                BuildingId = buildingId,
                BuildingName = "Updated Building",
                Status = "Inactive",
                FacilityId = 2 // Assuming FacilityId is valid
            };

            // Act
            _mockBuildingRepository.Object.UpdateBuilding(buildingToUpdate);

            // Assert
            _mockBuildingRepository.Verify(repo => repo.UpdateBuilding(It.IsAny<BuildingUpdateDTO>()), Times.Once);
        }

        [Fact]
        public void DeleteBuilding_ValidBuildingId_DeletesBuilding()
        {
            // Arrange
            int buildingId = 1;

            // Act
            _mockBuildingRepository.Object.DeleteBuilding(buildingId);

            // Assert
            _mockBuildingRepository.Verify(repo => repo.DeleteBuilding(buildingId), Times.Once);
        }
    }
}
