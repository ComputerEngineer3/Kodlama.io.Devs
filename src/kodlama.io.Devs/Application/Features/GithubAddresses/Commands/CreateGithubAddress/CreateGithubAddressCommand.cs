using Application.Features.GithubAddresses.Dtos;
using Application.Features.GithubAddresses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Authentication;
using Core.Security.Extensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Application.Features.GithubAddresses.Commands.CreateGithubAddress
{
    public class CreateGithubAddressCommand : IRequest<CreatedGithubAddressDto>
    {
        public int UserId { get; set; }
        public string GithubUrl { get; set; }

        public class CreateGithubAddressCommandHandler : IRequestHandler<CreateGithubAddressCommand, CreatedGithubAddressDto>
        {
            private readonly IGithubAddressRepository _githubAddressRepository;
            private readonly IMapper _mapper;
            private readonly GithubAddressBusinessRules _githubAddressBusinessRules;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateGithubAddressCommandHandler(IGithubAddressRepository githubAddressRepository, IMapper mapper, GithubAddressBusinessRules githubAddressBusinessRules, IHttpContextAccessor httpContextAccessor)
            { 
                _githubAddressRepository = githubAddressRepository;
                _mapper = mapper;
                _githubAddressBusinessRules = githubAddressBusinessRules;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<CreatedGithubAddressDto> Handle(CreateGithubAddressCommand request, CancellationToken cancellationToken)
            {
                await _githubAddressBusinessRules.GithubUrlCanNotBeDuplicatedWhenInserted(request.GithubUrl);

                GithubAddress mappedGithubAddress = _mapper.Map<GithubAddress>(request);
                GithubAddress createdGithubAddress = await _githubAddressRepository.AddAsync(mappedGithubAddress);
                CreatedGithubAddressDto createdGithubAddressDto = _mapper.Map<CreatedGithubAddressDto>(createdGithubAddress);

                return createdGithubAddressDto;
            }
        }
    }
}
