namespace MailingList.Logic.Services
{
    internal interface IMailingGroupService : IService
    {
        public void CheckMailingGroupIsUnique(string name);
    }
}
