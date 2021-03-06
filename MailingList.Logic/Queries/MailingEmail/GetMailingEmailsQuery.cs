using MailingList.Logic.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;

namespace MailingList.Logic.Queries.MailingEmail
{
    public class GetMailingEmailsQuery : IRequest<List<MailingEmailModel>>
    {
        public Guid UserId { get; set; }
        public Guid MailingGroupId { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
