using Application.Features.GithubAddresses.Dtos;
using Application.Features.GithubAddresses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubAddresses.Commands.UpdateGithubAddress
{
    public class UpdateGithubAddressCommand : IRequest<UpdatedGithubAddressDto>
    {
        public int Id { get; set; }
        public string GithubUrl { get; set; }

        public class UpdateGithubAddressCommandHandler : IRequestHandler<UpdateGithubAddressCommand, UpdatedGithubAddressDto>
        {
            private readonly IGithubAddressRepository _githubAddressRepository;
            private readonly IMapper _mapper;
            private readonly GithubAddressBusinessRules _githubAddressBusinessRules;

            public UpdateGithubAddressCommandHandler(IGithubAddressRepository githubAddressRepository, IMapper mapper, GithubAddressBusinessRules githubAddressBusinessRules)
            {
                _githubAddressRepository = githubAddressRepository;
                _mapper = mapper;
                _githubAddressBusinessRules = githubAddressBusinessRules;
            }

            public async Task<UpdatedGithubAddressDto> Handle(UpdateGithubAddressCommand request, CancellationToken cancellationToken)
            {
                await _githubAddressBusinessRules.GithubUrlCanNotBeDuplicatedWhenInserted(request.GithubUrl);

                GithubAddress? githubAddress = await _githubAddressRepository.GetAsync(b => b.Id == request.Id);
                githubAddress.GithubUrl = request.GithubUrl;
                GithubAddress updatedGithubAddress = await _githubAddressRepository.UpdateAsync(githubAddress);
                UpdatedGithubAddressDto updatedGithubAddressDto = _mapper.Map<UpdatedGithubAddressDto>(updatedGithubAddress);

                return updatedGithubAddressDto;
            }
        }
    }
}
