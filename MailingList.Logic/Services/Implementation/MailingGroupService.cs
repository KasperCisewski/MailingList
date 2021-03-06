using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Data;
using System;
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
            if (_mailingGroupRepository.GetAll().Any(mg => string.Equals(mg.Name, name, StringComparison.OrdinalIgnoreCase)))
                throw new LogicException(LogicErrorCode.MailingGroupNameMustBeUnique, $"Name {name} is not unique name for mailing group.");
        }
    }
}
