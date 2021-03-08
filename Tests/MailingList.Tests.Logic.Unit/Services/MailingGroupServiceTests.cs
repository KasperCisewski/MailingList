using MailingList.Data.Domains;
using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Data;
using MailingList.Logic.Services;
using MailingList.Logic.Services.Implementation;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MailingList.Tests.Logic.Unit.Services
{
    public class MailingGroupServiceTests
    {
        private IMailingGroupService _mailingGroupService;
        public MailingGroupServiceTests()
        {
            MockMailngGroupService();
        }

        private void MockMailngGroupService()
        {
            var mockMailingGroupRepository = new Mock<IMailingGroupRepository>();

            mockMailingGroupRepository.Setup(m => m.GetAll()).Returns(new List<MailingGroup>
            {
                new MailingGroup()
                {
                    Name = "testMailingGroup"
                },
                new MailingGroup()
                {
                    Name = "testMailingGroup2"
                },
                new MailingGroup()
                {
                    Name = "testMailingGroup3"
                },
            }.AsQueryable());

            _mailingGroupService = new MailingGroupService(mockMailingGroupRepository.Object);
        }

        [Fact]
        public void CheckIfMailingGroupNameIsUnique_MailingGroupNameUniqueInDatabase_ShouldWorkWithoutError()
        {
            _mailingGroupService.CheckMailingGroupIsUnique("testMailingGroup5");
        }

        [Fact]
        public void CheckIfMailingGroupNameIsUnique_MailingGroupNameIsNotUniqueInDatabase_ShouldThrowLogicException()
        {
            var exception = Assert.Throws<LogicException>(() => _mailingGroupService.CheckMailingGroupIsUnique("testMailingGroup3"));

            Assert.Equal(LogicErrorCode.MailingGroupNameMustBeUnique, exception.ErrorCode);
        }
    }
}
