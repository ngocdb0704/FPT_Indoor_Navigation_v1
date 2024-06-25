using BusinessObject.Models;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;

namespace ProjectTest.DAOTest
{
    public class BuildingDAOTest
    {

       
        [Fact]
        public void AddBuilding_Should_Add_New_Building()
        {
            // Condition: None

            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "fins")
                .Options;

            using (var context = new finsContext(options))
            {
                var buildingDAO = new BuildingDAO(context);

                // Trường hợp tên tòa nhà là rỗng
                var newBuilding = new Building { BuildingName = "", Status = "", Image = "", FacilityId = 1 };

                // Act
                buildingDAO.AddBuilding(newBuilding);

                // Confirm: Building is added to the database
                var addedBuilding = context.Buildings.FirstOrDefault(b => b.BuildingName == "");

                // Assert
                Assert.NotNull(addedBuilding);
                Assert.Equal("", addedBuilding.BuildingName);
                Assert.Equal("", addedBuilding.Image);
                Assert.Equal("", addedBuilding.Status);
            }

        }

        [Fact]
        public void GetBuildingById_Should_Return_Building_With_Correct_Id()
        {
            // Condition: Building with specified Id exists in the database

            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "fins")
                .Options;

            using (var context = new finsContext(options))
            {
                var buildingDAO = new BuildingDAO(context);
                var newBuilding = new Building { BuildingId = 1, BuildingName = "Test Building", Status = "Active", Image = "image-url", FacilityId = 1 };
                context.Buildings.Add(newBuilding);
                context.SaveChanges();

                // Act
                var retrievedBuilding = buildingDAO.GetBuildingById(1);

                // Confirm: Retrieved building has correct Id and details
                Assert.NotNull(retrievedBuilding);
                Assert.Equal("Test Building", retrievedBuilding.BuildingName);
                Assert.Equal(1, retrievedBuilding.BuildingId);
            }

            // Result: Building retrieved successfully
        }

        [Fact]
        public void GetBuildingById_Should_Return_Building_With_Null_Details()
        {
            // Condition: Building with specified Id exists in the database but has null details

            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "fins")
                .Options;

            using (var context = new finsContext(options))
            {
                var buildingDAO = new BuildingDAO(context);
                var newBuilding = new Building { BuildingId = 2, BuildingName = "", Status = "", Image = "", FacilityId = 1 };
                context.Buildings.Add(newBuilding);
                context.SaveChanges();

                // Act
                var retrievedBuilding = buildingDAO.GetBuildingById(2);

                // Assert
                Assert.NotNull(retrievedBuilding);
                Assert.Equal("", retrievedBuilding.BuildingName); 
                Assert.Equal("", retrievedBuilding.Status);
                Assert.Equal("", retrievedBuilding.Image);
            }
        }

        [Fact]
        public void GetBuildingById_Should_Throw_Exception_For_Invalid_Id()
        {
            // Condition: None

            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "fins")
                .Options;

            using (var context = new finsContext(options))
            {
                var buildingDAO = new BuildingDAO(context);

                // Act & Assert
                Assert.Throws<ArgumentException>(() => buildingDAO.GetBuildingById(-1)); // Invalid ID
            }
        }
        [Fact]
        public void UpdateBuilding_Should_Update_Building_Information()
        {
            // Condition: Building with specified Id exists in the database

            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "fins")
                .Options;

            using (var context = new finsContext(options))
            {
                var buildingDAO = new BuildingDAO(context);
                var newBuilding = new Building { BuildingId = 1, BuildingName = "Test Building", Status = "Active", Image = "image-url", FacilityId = 1 };
                context.Buildings.Add(newBuilding);
                context.SaveChanges();

                // Act
                newBuilding.BuildingName = "Updated Building Name";
                buildingDAO.UpdateBuilding(newBuilding);

                // Confirm: Building information updated in the database
                var updatedBuilding = context.Buildings.FirstOrDefault(b => b.BuildingId == 1);
                Assert.NotNull(updatedBuilding);
                Assert.Equal("Updated Building Name", updatedBuilding.BuildingName);
            }

            // Result: Building information updated successfully
        }
       
        [Fact]
        public void DeleteBuilding_Should_Remove_Building_From_Database()
        {
            // Condition: Building with specified Id exists in the database

            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "fins")
                .Options;

            using (var context = new finsContext(options))
            {
                var buildingDAO = new BuildingDAO(context);
                var newBuilding = new Building { BuildingId = 1, BuildingName = "Test Building", Status = "Active", Image = "image-url", FacilityId = 1 };
                context.Buildings.Add(newBuilding);
                context.SaveChanges();

                // Act
                buildingDAO.DeleteBuilding(1);

                // Confirm: Building removed from the database
                var deletedBuilding = context.Buildings.FirstOrDefault(b => b.BuildingId == 1);
                Assert.Null(deletedBuilding);
            }

            // Result: Building removed successfully
        }

        [Fact]
        public void GetAllBuildings_Should_Return_All_Buildings_With_Success_Log()
        {
            // Condition: Buildings exist in the database

            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "fins")
                .Options;

            List<Building> buildingsOutput = new List<Building>();

            using (var context = new finsContext(options))
            {
                var facility = new Facility
                {
                    FacilityName = "Test Facility",
                    Address = "Test Address",
                    Status = "Active"
                };
                context.Facilities.Add(facility);
                context.SaveChanges();

                context.Buildings.Add(new Building { BuildingName = "Building 1", Status = "Active", Image = "image-url", FacilityId = facility.FacilityId });
                context.Buildings.Add(new Building { BuildingName = "Building 2", Status = "Inactive", Image = "image-url", FacilityId = facility.FacilityId });
                context.SaveChanges();

                var buildingDAO = new BuildingDAO(context);

                // Act
                buildingsOutput = buildingDAO.GetAllBuildings();

                // Confirm: All buildings retrieved from the database
                Assert.Equal(2, buildingsOutput.Count);
                Assert.Contains(buildingsOutput, b => b.BuildingName == "Building 1");
                Assert.Contains(buildingsOutput, b => b.BuildingName == "Building 2");
            }

            // Result: All buildings retrieved successfully
        }

    }
}