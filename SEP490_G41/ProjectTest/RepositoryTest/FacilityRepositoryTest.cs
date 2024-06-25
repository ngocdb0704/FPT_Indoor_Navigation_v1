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
    public class FacilityRepositoryTest
    {
        public class FacilityRepositoryTests
        {
            private Mock<IFacilityRepository> _mockFacilityRepository;
            private IMapper _mapper;

            public FacilityRepositoryTests()
            {
                _mockFacilityRepository = new Mock<IFacilityRepository>();
                _mapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Facility, FacilityDTO>();
                    // Add other mappings if needed
                }).CreateMapper();
            }

            [Fact]
            public void GetFacilityById_FacilityExists_ReturnsFacilityDTO()
            {
                // Arrange
                int facilityId = 1;
                var facilityDTO = new FacilityDTO { FacilityId = facilityId, FacilityName = "Facility 1" };

                _mockFacilityRepository.Setup(repo => repo.GetFacilityById(facilityId)).Returns(facilityDTO);

                // Act
                var result = _mockFacilityRepository.Object.GetFacilityById(facilityId);

                // Assert
                Assert.Equal(facilityDTO, result);
            }

            [Fact]
            public void GetAllFacilities_ReturnsListOfFacilityDTOs()
            {
                // Arrange
                var facilities = new List<FacilityDTO>
            {
                new FacilityDTO { FacilityId = 1, FacilityName = "Facility 1" },
                new FacilityDTO { FacilityId = 2, FacilityName = "Facility 2" }
            };

                _mockFacilityRepository.Setup(repo => repo.GetAllFacilities()).Returns(facilities);

                // Act
                var result = _mockFacilityRepository.Object.GetAllFacilities();

                // Assert
                Assert.Equal(facilities.Count, result.Count);
                Assert.All(result, f => Assert.Contains(f, facilities));
            }

            [Fact]
            public void AddFacility_ValidFacility_AddsFacility()
            {
                // Arrange
                var facilityToAdd = new FacilityAddDTO
                {
                    FacilityName = "New Facility",
                    Address = "123 Main Street",
                    Status = "Active"
                };

                // Act
                _mockFacilityRepository.Object.AddFacility(facilityToAdd);

                // Assert
                _mockFacilityRepository.Verify(repo => repo.AddFacility(It.IsAny<FacilityAddDTO>()), Times.Once);
            }

            [Fact]
            public void UpdateFacility_ValidFacility_UpdatesFacility()
            {
                // Arrange
                int facilityId = 1;
                var facilityToUpdate = new FacilityUpdateDTO
                {
                    FacilityId = facilityId,
                    FacilityName = "Updated Facility",
                    Address = "456 Elm Street",
                    Status = "Inactive"
                };

                // Act
                _mockFacilityRepository.Object.UpdateFacility(facilityToUpdate);

                // Assert
                _mockFacilityRepository.Verify(repo => repo.UpdateFacility(It.IsAny<FacilityUpdateDTO>()), Times.Once);
            }

            [Fact]
            public void DeleteFacility_ValidFacilityId_DeletesFacility()
            {
                // Arrange
                int facilityId = 1;

                // Act
                _mockFacilityRepository.Object.DeleteFacility(facilityId);

                // Assert
                _mockFacilityRepository.Verify(repo => repo.DeleteFacility(facilityId), Times.Once);
            }

        }
    }
}
