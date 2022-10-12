using Application.Features.UserOperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Queries.GetListByUserIdUserOperationClaim
{
    public class GetListByUserIdUserOperationClaimQuery : IRequest<UserOperationClaimListModel>
    {
        public int UserId { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListByUserIdUserOperationClaimQueryHandler : IRequestHandler<GetListByUserIdUserOperationClaimQuery, UserOperationClaimListModel>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;

            public GetListByUserIdUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
            }

            public async Task<UserOperationClaimListModel> Handle(GetListByUserIdUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(b=>b.UserId==request.UserId, index:request.PageRequest.Page, size:request.PageRequest.PageSize);

                UserOperationClaimListModel mappedUserOperationClaimListModel = _mapper.Map<UserOperationClaimListModel>(userOperationClaims);

                return mappedUserOperationClaimListModel;
            }
        }
    }
}
