using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubAddresses.Rules
{
    public class GithubAddressBusinessRules
    {
        private readonly IGithubAddressRepository _githubAddressRepository;

        public GithubAddressBusinessRules(IGithubAddressRepository githubAddressRepository)
        {
            _githubAddressRepository = githubAddressRepository;
        }

        public async Task GithubUrlCanNotBeDuplicatedWhenInserted(string githubUrl)
        {
            IPaginate<GithubAddress> result = await _githubAddressRepository.GetListAsync(b => b.GithubUrl == githubUrl);
            if (result.Items.Any()) throw new BusinessException("GithubUrl exists.");
        }

        public void UserIdCanNotBeNullWhenInserted(int userId)
        {
            if (userId == null) throw new BusinessException("UserId can not be empty.");
        }

    }
}
