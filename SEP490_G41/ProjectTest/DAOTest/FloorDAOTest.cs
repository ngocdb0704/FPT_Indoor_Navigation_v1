using BusinessObject.Models;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTest.DAOTest
{
    public class FloorDAOTest
    {
        [Fact]
        public void AddFloor_Should_Add_New_Floor()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "AddFloor_Should_Add_New_Floor")
                .Options;

            using (var context = new finsContext(options))
            {
                var floorDAO = new FloorDAO(context);

                // Act
                var newFloor = new Floor { FloorName = "Test Floor", Greeting = "Welcome!", Status = "Active", BuildingId = 1 };
                floorDAO.AddFloor(newFloor);

                // Assert
                var addedFloor = context.Floors.FirstOrDefault(f => f.FloorName == "Test Floor");
                Assert.NotNull(addedFloor);
            }
        }

        [Fact]
        public void GetFloorById_Should_Return_Correct_Floor()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "GetFloorById_Should_Return_Correct_Floor")
                .Options;

            using (var context = new finsContext(options))
            {
                var floorDAO = new FloorDAO(context);
                var newFloor = new Floor { FloorId = 1, FloorName = "Test Floor", Greeting = "Welcome!", Status = "Active", BuildingId = 1 };
                context.Floors.Add(newFloor);
                context.SaveChanges();

                // Act
                var retrievedFloor = floorDAO.GetFloorById(1);

                // Assert
                Assert.NotNull(retrievedFloor);
                Assert.Equal("Test Floor", retrievedFloor.FloorName);
            }
        }

        [Fact]
        public void UpdateFloor_Should_Update_Floor_Information()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "UpdateFloor_Should_Update_Floor_Information")
                .Options;

            using (var context = new finsContext(options))
            {
                var floorDAO = new FloorDAO(context);
                var newFloor = new Floor { FloorId = 1, FloorName = "Test Floor", Greeting = "Welcome!", Status = "Active", BuildingId = 1 };
                context.Floors.Add(newFloor);
                context.SaveChanges();

                // Act
                newFloor.FloorName = "Updated Floor";
                floorDAO.UpdateFloor(newFloor);

                // Assert
                var updatedFloor = context.Floors.FirstOrDefault(f => f.FloorId == 1);
                Assert.NotNull(updatedFloor);
                Assert.Equal("Updated Floor", updatedFloor.FloorName);
            }
        }

        [Fact]
        public void DeleteFloor_Should_Remove_Floor_From_Database()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "DeleteFloor_Should_Remove_Floor_From_Database")
                .Options;

            using (var context = new finsContext(options))
            {
                var floorDAO = new FloorDAO(context);
                var newFloor = new Floor { FloorId = 1, FloorName = "Test Floor", Greeting = "Welcome!", Status = "Active", BuildingId = 1 };
                context.Floors.Add(newFloor);
                context.SaveChanges();

                // Act
                floorDAO.DeleteFloor(1);

                // Assert
                var deletedFloor = context.Floors.FirstOrDefault(f => f.FloorId == 1);
                Assert.Null(deletedFloor);
            }
        }

        [Fact]
        public void GetAllFloors_Should_Return_All_Floors()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "GetAllFloors_Should_Return_All_Floors")
                .Options;

            using (var context = new finsContext(options))
            {
                var floorDAO = new FloorDAO(context);
                var newFloor1 = new Floor { FloorName = "Floor 1", Greeting = "Welcome!", Status = "Active", BuildingId = 1 };
                var newFloor2 = new Floor { FloorName = "Floor 2", Greeting = "Welcome!", Status = "Active", BuildingId = 1 };
                context.Floors.Add(newFloor1);
                context.Floors.Add(newFloor2);
                context.SaveChanges();

                // Act
                var floors = floorDAO.GetAllFloors();

                // Assert
                Assert.NotNull(floors);
                Assert.Equal(2, floors.Count);
            }
        }
    }
}
