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

namespace Application.Features.GithubAddresses.Commands.DeleteGithubAddress
{
    public class DeleteGithubAddressCommand : IRequest<DeletedGithubAddressDto>
    {
        public int Id { get; set; }

        public class DeleteGithubAddressCommandHandler : IRequestHandler<DeleteGithubAddressCommand, DeletedGithubAddressDto>
        {
            private readonly IGithubAddressRepository _githubAddressRepository;
            private readonly IMapper _mapper;
            private readonly GithubAddressBusinessRules _githubAddressBusinessRules;

            public DeleteGithubAddressCommandHandler(IGithubAddressRepository githubAddressRepository, IMapper mapper, GithubAddressBusinessRules githubAddressBusinessRules)
            {
                _githubAddressRepository = githubAddressRepository;
                _mapper = mapper;
                _githubAddressBusinessRules = githubAddressBusinessRules;
            }

            public async Task<DeletedGithubAddressDto> Handle(DeleteGithubAddressCommand request, CancellationToken cancellationToken)
            {
                GithubAddress? githubAddress = await _githubAddressRepository.GetAsync(b => b.Id == request.Id);

                GithubAddress deletedGithubAddress = await _githubAddressRepository.DeleteAsync(githubAddress);
                DeletedGithubAddressDto deletedGithubAddressDto = _mapper.Map<DeletedGithubAddressDto>(deletedGithubAddress);

                return deletedGithubAddressDto;
            }
        }
    }
}
