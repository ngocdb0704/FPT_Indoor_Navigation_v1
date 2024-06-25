using Xunit;
using DataAccess.DAO;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ProjectTest.DAOTest
{
    public class ProfileDAOTests
    {
        private finsContext _context;
        private ProfileDAO _profileDAO;

        public ProfileDAOTests()
        {
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "fins_test")
                .Options;
            _context = new finsContext(options);
            _profileDAO = new ProfileDAO(_context);
        }

        [Fact]
        public void GetMemberById_WhenMemberExists_ReturnsMember()
        {
            // Arrange
            var member = new Member { MemberId = 1, FullName = "John Doe", DoB = new DateTime(1990, 1, 1), Address = "123 Main St", Phone = "123456789", Email = "john@example.com", Username = "123", Password = "12345", Status = "Active" };
            _context.Members.Add(member);
            _context.SaveChanges();

            // Act
            var result = _profileDAO.GetMemberById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.MemberId);
            Assert.Equal("John Doe", result.FullName);

            // Add more assertions for other properties
        }

        [Fact]
        public void GetMemberById_WhenMemberDoesNotExist_ReturnsNull()
        {
            // Act
            var result = _profileDAO.GetMemberById(999);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public void UpdateProfile_WhenMemberExists_UpdatesProfile()
        {
            // Arrange
            var member = new Member { MemberId = 1, FullName = "Jane Smith", DoB = new DateTime(1985, 5, 5), Address = "456 Elm St", Phone = "987654321", Email = "jane@example.com", Username = "123", Password = "12345", Status = "Active"};
            _context.Members.Add(member);
            _context.SaveChanges();

            // Act
            var updatedMember = new Member { MemberId = 1, FullName = "Jane Johnson" };
            _profileDAO.UpdateProfile(updatedMember);

            // Assert
            var retrievedMember = _context.Members.FirstOrDefault(m => m.MemberId == 1);
            Assert.NotNull(retrievedMember);
            Assert.Equal("Jane Johnson", retrievedMember.FullName);
            Assert.Equal(member.DoB, retrievedMember.DoB);
            Assert.Equal(member.Address, retrievedMember.Address);
            Assert.Equal(member.Phone, retrievedMember.Phone);
            Assert.Equal(member.Email, retrievedMember.Email);
            // Add more assertions for other properties
        }


        [Fact]
        public void UpdateProfile_WhenMemberDoesNotExist_DoesNotUpdateProfile()
        {
            // Arrange
            var member = new Member { MemberId = 999, FullName = "Test User", DoB = new DateTime(1999, 9, 9), Address = "789 Oak St", Phone = "555555555", Email = "test@example.com" };

            // Act
            _profileDAO.UpdateProfile(member);

            // Assert
            var updatedMember = _context.Members.FirstOrDefault(m => m.MemberId == 999);
            Assert.Null(updatedMember);
        }
    }
}
