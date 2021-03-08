using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Data;
using System.Linq;

namespace MailingList.Logic.Services.Implementation
{
    internal class MailingGroupService : IMailingGroupService
    {
        private readonly IMailingGroupRepository _mailingGroupRepository;

        public MailingGroupService(IMailingGroupRepository mailingGroupRepository)
        {
            _mailingGroupRepository = mailingGroupRepository;
        }

        public void CheckMailingGroupIsUnique(string name)
        {
            if (_mailingGroupRepository.GetAll().Any(mg => mg.Name.ToLower() == name.ToLower()))
                throw new LogicException(LogicErrorCode.MailingGroupNameMustBeUnique, $"Name {name} is not unique name for mailing group.");
        }
    }
}
