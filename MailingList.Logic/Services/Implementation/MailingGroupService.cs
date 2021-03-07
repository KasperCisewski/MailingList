using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Data;
using System.Linq;

namespace MailingList.Logic.Services.Implementation
{
    internal class MailingGroupService : IMailingGroupService
    {
        private readonly IMailingGroupRepository _mailingGroupRepository;
        private readonly IMailingEmailGroupRepository _mailingEmailGroupRepository;

        public MailingGroupService(IMailingGroupRepository mailingGroupRepository, IMailingEmailGroupRepository mailingEmailGroupRepository)
        {
            _mailingGroupRepository = mailingGroupRepository;
            _mailingEmailGroupRepository = mailingEmailGroupRepository;
        }

        public void CheckMailingGroupIsUnique(string name)
        {
            if (_mailingGroupRepository.GetAll().Any(mg => mg.Name.ToLower() == name.ToLower()))
                throw new LogicException(LogicErrorCode.MailingGroupNameMustBeUnique, $"Name {name} is not unique name for mailing group.");
        }
    }
}
