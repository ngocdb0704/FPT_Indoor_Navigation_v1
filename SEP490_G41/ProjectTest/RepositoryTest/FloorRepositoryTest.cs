using BusinessObject.DTO;
using DataAccess.IRepository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTest.RepositoryTest
{
    public class FloorRepositoryTest
    {
        [Fact]
        public void GetFloorById_ReturnsCorrectFloor()
        {
            // Arrange
            var mockRepository = new Mock<IFloorRepository>();
            var expectedFloor = new FloorDTO { FloorId = 1, FloorName = "Floor 1", Greeting = "Welcome", Status = "Active", BuildingId = 1 };
            mockRepository.Setup(repo => repo.GetFloorById(1)).Returns(expectedFloor);
            var repository = mockRepository.Object;

            // Act
            var result = repository.GetFloorById(1);

            // Assert
            Assert.Equal(expectedFloor, result);
        }

        [Fact]
        public void GetAllFloors_ReturnsListOfFloors()
        {
            // Arrange
            var mockRepository = new Mock<IFloorRepository>();
            var expectedFloors = new List<FloorDTO>
        {
            new FloorDTO { FloorId = 1, FloorName = "Floor 1", Greeting = "Welcome", Status = "Active", BuildingId = 1 },
            new FloorDTO { FloorId = 2, FloorName = "Floor 2", Greeting = "Hello", Status = "Inactive", BuildingId = 1 }
        };
            mockRepository.Setup(repo => repo.GetAllFloors()).Returns(expectedFloors);
            var repository = mockRepository.Object;

            // Act
            var result = repository.GetAllFloors();

            // Assert
            Assert.Equal(expectedFloors, result);
        }

        [Fact]
        public void AddFloor_SuccessfullyAddsNewFloor()
        {
            // Arrange
            var mockRepository = new Mock<IFloorRepository>();
            var newFloor = new FloorAddDTO { FloorName = "New Floor", Greeting = "Welcome", Status = "Active", BuildingId = 1 };

            // Act
            mockRepository.Object.AddFloor(newFloor);

            // Assert
            mockRepository.Verify(repo => repo.AddFloor(newFloor), Times.Once);
        }

        [Fact]
        public void UpdateFloor_SuccessfullyUpdatesFloor()
        {
            // Arrange
            var mockRepository = new Mock<IFloorRepository>();
            var floorToUpdate = new FloorDTO { FloorId = 1, FloorName = "Updated Floor", Greeting = "Welcome", Status = "Active", BuildingId = 1 };

            // Act
            mockRepository.Object.UpdateFloor(floorToUpdate);

            // Assert
            mockRepository.Verify(repo => repo.UpdateFloor(floorToUpdate), Times.Once);
        }

        [Fact]
        public void DeleteFloor_SuccessfullyDeletesFloor()
        {
            // Arrange
            var mockRepository = new Mock<IFloorRepository>();
            var floorIdToDelete = 1;

            // Act
            mockRepository.Object.DeleteFloor(floorIdToDelete);

            // Assert
            mockRepository.Verify(repo => repo.DeleteFloor(floorIdToDelete), Times.Once);
        }
    }
}
