using MailingList.Logic.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;

namespace MailingList.Logic.Queries.MailingGroup
{
    public class GetMailingGroupsQuery : IRequest<List<MailingGroupModel>>
    {
        public Guid UserId { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
