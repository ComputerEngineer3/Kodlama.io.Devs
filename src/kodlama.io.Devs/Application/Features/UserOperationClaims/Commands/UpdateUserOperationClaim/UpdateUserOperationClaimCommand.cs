using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaim
{
    public class UpdateUserOperationClaimCommand : IRequest<UpdatedUserOperationClaimDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommand, UpdatedUserOperationClaimDto>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public UpdateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<UpdatedUserOperationClaimDto> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(b => b.Id == request.Id);

                _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenRequested(userOperationClaim);

                userOperationClaim.UserId = request.UserId;
                userOperationClaim.OperationClaimId = request.OperationClaimId;
                UserOperationClaim updatedUserOperationClaim = await _userOperationClaimRepository.UpdateAsync(userOperationClaim);
                UpdatedUserOperationClaimDto updatedUserOperationClaimDto = _mapper.Map<UpdatedUserOperationClaimDto>(updatedUserOperationClaim);

                return updatedUserOperationClaimDto;
            }
        }
    }
}
