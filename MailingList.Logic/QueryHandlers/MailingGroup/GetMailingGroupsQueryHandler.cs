﻿using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Models.Responses;
using MailingList.Logic.Queries.MailingGroup;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MailingList.Logic.QueryHandlers.MailingGroup
{
    public class GetMailingGroupsQueryHandler : IRequestHandler<GetMailingGroupsQuery, List<MailingGroupModel>>
    {
        private readonly IMailingGroupRepository _mailingGroupRepository;

        public GetMailingGroupsQueryHandler(IMailingGroupRepository mailingGroupRepository)
        {
            _mailingGroupRepository = mailingGroupRepository;
        }

        public async Task<List<MailingGroupModel>> Handle(GetMailingGroupsQuery request, CancellationToken cancellationToken)
        {
            return await _mailingGroupRepository.GetAll()
                        .Where(mg => mg.UserId == request.UserId)
                        .Take(request.Take)
                        .Skip(request.Skip)
                        .Select(mg => new MailingGroupModel()
                        {
                            Id = mg.Id,
                            Name = mg.Name
                        }).ToListAsync();
        }
    }
}