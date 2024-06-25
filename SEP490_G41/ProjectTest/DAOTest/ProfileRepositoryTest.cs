
﻿//using Xunit;
//using AutoMapper;
//using BusinessObject.DTO;
//using BusinessObject.Models;
//using DataAccess.DAO;
//using DataAccess.IRepository.Repository;
//using Microsoft.EntityFrameworkCore;
//using System;

//namespace DataAccess.IRepository.Repository.Tests
//{
//    public class ProfileRepositoryTests
//    {
//        [Fact]
//        public void GetMemberById_ValidId_ReturnsMemberDTO()
//        {
//            // Arrange
//            int memberId = 1;
//            var options = new DbContextOptionsBuilder<finsContext>()
//                .UseInMemoryDatabase(databaseName: "TestDB")
//                .Options;
//            using (var context = new finsContext(options))
//            {
//                var member = new Member { MemberId = 1, FullName = "John Doe", DoB = new DateTime(1990, 1, 1), Address = "123 Main St", Phone = "123456789", Email = "john@example.com", Username = "123", Password = "12345", Status = "Active" };
//                context.Members.Add(member);
//                context.SaveChanges();

//                var config = new MapperConfiguration(cfg =>
//                {
//                    cfg.CreateMap<Member, MemberDTO>();
//                });
//                IMapper mapper = config.CreateMapper();

//                var profileDAO = new ProfileDAO(context);
//                var profileRepository = new ProfileRepository(profileDAO, mapper);

//                // Act
//                var result = profileRepository.GetMemberById(memberId);

//                // Assert
//                Assert.NotNull(result);
//                // Add more assertions as needed
//            }
//        }

//        [Fact]
//        public void GetMemberById_InvalidId_ThrowsException()
//        {
//            // Arrange
//            int invalidMemberId = -1;
//            var options = new DbContextOptionsBuilder<finsContext>()
//                .UseInMemoryDatabase(databaseName: "TestDB")
//                .Options;
//            using (var context = new finsContext(options))
//            {
//                var config = new MapperConfiguration(cfg =>
//                {
//                    cfg.CreateMap<Member, MemberDTO>();
//                });
//                IMapper mapper = config.CreateMapper();

//                var profileDAO = new ProfileDAO(context);
//                var profileRepository = new ProfileRepository(profileDAO, mapper);

//                // Act & Assert
//                Assert.Throws<Exception>(() => profileRepository.GetMemberById(invalidMemberId));
//            }
//        }

//        [Fact]
//        public void UpdateProfile_ValidProfile_NoExceptionThrown()
//        {
//            // Arrange
//            var memberUpdateDTO = new MemberUpdateDTO { /* create a valid member update DTO */ };
//            var options = new DbContextOptionsBuilder<finsContext>()
//                .UseInMemoryDatabase(databaseName: "TestDB")
//                .Options;
//            using (var context = new finsContext(options))
//            {
//                var config = new MapperConfiguration(cfg =>
//                {
//                    cfg.CreateMap<MemberUpdateDTO, Member>();
//                });
//                IMapper mapper = config.CreateMapper();
//
//                var profileDAO = new ProfileDAO(context);
//                var profileRepository = new ProfileRepository(profileDAO, mapper);
//
//                // Act & Assert
//                Assert.Null(Record.Exception(() => profileRepository.UpdateProfile(memberUpdateDTO)));
//            }
//        }

//        [Fact]
//        public void ChangePassword_ValidPassword_NoExceptionThrown()
//        {
//            // Arrange
//            var changePasswordDTO = new ChangePasswordDTO { /* create a valid change password DTO */ };
//            var options = new DbContextOptionsBuilder<finsContext>()
//                .UseInMemoryDatabase(databaseName: "TestDB")
//                .Options;
//            using (var context = new finsContext(options))
//            {
//                var config = new MapperConfiguration(cfg =>
//                {
//                    cfg.CreateMap<ChangePasswordDTO, Member>();
//                });
//                IMapper mapper = config.CreateMapper();

//                var profileDAO = new ProfileDAO(context);
//                var profileRepository = new ProfileRepository(profileDAO, mapper);

//                // Act & Assert
//                Assert.Null(Record.Exception(() => profileRepository.ChangePassword(changePasswordDTO)));
//            }
//        }
//    }
//}
     /*   [Fact]
        public void ChangePassword_ValidPassword_NoExceptionThrown()
        {
            // Arrange
            var changePasswordDTO = new ChangePasswordDTO { *//* create a valid change password DTO *//* };
            var options = new DbContextOptionsBuilder<finsContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;
            using (var context = new finsContext(options))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ChangePasswordDTO, Member>();
                });
                IMapper mapper = config.CreateMapper();

                var profileDAO = new ProfileDAO(context);
                var profileRepository = new ProfileRepository(profileDAO, mapper);

                // Act & Assert
                Assert.Null(Record.Exception(() => profileRepository.ChangePassword(changePasswordDTO)));
            }
        }*/
