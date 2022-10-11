using Application.Features.GithubAddresses.Commands.CreateGithubAddress;
using Application.Features.GithubAddresses.Commands.DeleteGithubAddress;
using Application.Features.GithubAddresses.Commands.UpdateGithubAddress;
using Application.Features.GithubAddresses.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubAddressesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGithubAddressCommand createGithubAddressCommand)
        {
            CreatedGithubAddressDto result = await Mediator.Send(createGithubAddressCommand);
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGithubAddressCommand updateGithubAddressCommand)
        {
            UpdatedGithubAddressDto result = await Mediator.Send(updateGithubAddressCommand);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteGithubAddressCommand deleteGithubAddressCommand)
        {
            DeletedGithubAddressDto deletedGithubAddressDto = await Mediator.Send(deleteGithubAddressCommand);
            return Ok(deletedGithubAddressDto);
        }
    }
}
