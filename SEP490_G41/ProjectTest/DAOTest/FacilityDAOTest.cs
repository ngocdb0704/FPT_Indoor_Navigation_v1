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
    public class FacilityDAOTest
    {
        [Fact]
        public void AddFacility_Should_Add_New_Facility()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "test_facility")
                .Options;

            using (var context = new finsContext(options))
            {
                var facilityDAO = new FacilityDAO(context);
                var newFacility = new Facility { FacilityName = "Test Facility", Address = "123 Test St", Status = "Active" };

                // Act
                facilityDAO.AddFacility(newFacility);

                // Assert
                var addedFacility = context.Facilities.FirstOrDefault(f => f.FacilityName == "Test Facility");
                Assert.NotNull(addedFacility);
                Assert.Equal("Test Facility", addedFacility.FacilityName);
            }
        }

        [Fact]
        public void GetFacilityById_Should_Return_Correct_Facility()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "test_facility")
                .Options;

            using (var context = new finsContext(options))
            {
                var facilityDAO = new FacilityDAO(context);
                var newFacility = new Facility { FacilityName = "Test Facility", Address = "123 Test St", Status = "Active" };
                context.Facilities.Add(newFacility);
                context.SaveChanges();

                // Act
                var retrievedFacility = facilityDAO.GetFacilityById(newFacility.FacilityId);

                // Assert
                Assert.NotNull(retrievedFacility);
                Assert.Equal("Test Facility", retrievedFacility.FacilityName);
            }
        }

        [Fact]
        public void UpdateFacility_Should_Update_Facility_Information()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "test_facility")
                .Options;

            using (var context = new finsContext(options))
            {
                var facilityDAO = new FacilityDAO(context);
                var newFacility = new Facility { FacilityName = "Test Facility", Address = "123 Test St", Status = "Active" };
                context.Facilities.Add(newFacility);
                context.SaveChanges();

                // Act
                newFacility.FacilityName = "Updated Facility";
                facilityDAO.UpdateFacility(newFacility);

                // Assert
                var updatedFacility = context.Facilities.FirstOrDefault(f => f.FacilityId == newFacility.FacilityId);
                Assert.NotNull(updatedFacility);
                Assert.Equal("Updated Facility", updatedFacility.FacilityName);
            }
        }

        [Fact]
        public void DeleteFacility_Should_Delete_Facility()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "test_facility")
                .Options;

            using (var context = new finsContext(options))
            {
                var facilityDAO = new FacilityDAO(context);
                var newFacility = new Facility { FacilityName = "Test Facility", Address = "123 Test St", Status = "Active" };
                context.Facilities.Add(newFacility);
                context.SaveChanges();

                // Act
                facilityDAO.DeleteFacility(newFacility.FacilityId);

                // Assert
                var deletedFacility = context.Facilities.FirstOrDefault(f => f.FacilityId == newFacility.FacilityId);
                Assert.Null(deletedFacility);
            }
        }

        [Fact]
        public void GetAllFacilities_Should_Return_All_Facilities()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "test_facility")
                .Options;

            using (var context = new finsContext(options))
            {
                var facilityDAO = new FacilityDAO(context);
                var newFacility1 = new Facility { FacilityName = "Facility 1", Address = "123 Test St", Status = "Active" };
                var newFacility2 = new Facility { FacilityName = "Facility 2", Address = "456 Test St", Status = "Active" };
                context.Facilities.Add(newFacility1);
                context.Facilities.Add(newFacility2);
                context.SaveChanges();

                // Act
                var facilities = facilityDAO.GetAllFacilities();

                // Assert
                Assert.NotNull(facilities);
                Assert.Equal(2, facilities.Count);
            }
        }
        [Fact]
        public void GetFacilityById_Should_Return_Null_For_Nonexistent_Id()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "test_facility")
                .Options;

            using (var context = new finsContext(options))
            {
                var facilityDAO = new FacilityDAO(context);
                var newFacility = new Facility { FacilityName = "Test Facility", Address = "123 Test St", Status = "Active" };
                context.Facilities.Add(newFacility);
                context.SaveChanges();

                // Act
                var retrievedFacility = facilityDAO.GetFacilityById(newFacility.FacilityId + 1); // Nonexistent ID

                // Assert
                Assert.Null(retrievedFacility);
            }
        }

       
        [Fact]
        public void GetAllFacilities_Should_Return_Empty_List_When_No_Facilities_Are_Present()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "test_facility")
                .Options;

            using (var context = new finsContext(options))
            {
                var facilityDAO = new FacilityDAO(context);

                // Act
                var facilities = facilityDAO.GetAllFacilities();

                // Assert
                Assert.Empty(facilities);
            }
        }
    }
}

