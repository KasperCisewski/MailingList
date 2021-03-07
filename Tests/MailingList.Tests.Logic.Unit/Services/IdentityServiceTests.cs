using MailingList.Data.Domains;
using MailingList.Data.Repository.Abstraction;
using MailingList.Data.Repository.Implementation;
using MailingList.Logic.Services.Implementation;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MailingList.Tests.Logic.Unit.Services
{
    public class IdentityServiceTests
    {
        private IdentityService _identityService;
        public IdentityServiceTests()
        {
            MockIdentityService();
        }

        private void MockIdentityService()
        {
            var mockUsers = new List<User>()
            {
                new User()
                {
                    UserName = "test123",
                    Email = "test123@gmail.com"
                },
                new User()
                {
                    UserName = "test1234",
                    Email = "test1234@gmail.com"
                },
                new User()
                {
                    UserName = "test12345",
                    Email = "test12345@gmail.com"
                }
            };
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(m => m.GetAll()).Returns(mockUsers.AsQueryable());

            _identityService = new IdentityService(mockUserRepository.Object);
        }

        [Fact]
        public void CheckIfEmailIsUnique_EmailIsUniqueInDatabase_ShouldReturnFalse()
        {
            var result = _identityService.UserWithEmailExists("testEmail123@gmail.com");

            Assert.False(result);
        }

        [Fact]
        public void CheckIfEmailIsUnique_EmailIsNotUniqueInDatabase_ShouldReturnFalse()
        {
            var result = _identityService.UserWithEmailExists("test1234@gmail.com");

            Assert.True(result);
        }

        [Fact]
        public void CheckIfUsernameIsUnique_UsernameIsUniqueInDatabase_ShouldReturnFalse()
        {
            var result = _identityService.UserWithUsernameExists("testEmail123");

            Assert.False(result);
        }

        [Fact]
        public void CheckIfUsernameIsUnique_UsernameIsNotUniqueInDatabase_ShouldReturnFalse()
        {
            var result = _identityService.UserWithUsernameExists("test1234");

            Assert.True(result);
        }
    }
}
