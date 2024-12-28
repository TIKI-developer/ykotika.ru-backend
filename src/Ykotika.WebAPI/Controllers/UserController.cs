using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Ykotika.Application.Commands;
using Ykotika.Application.Interfaces;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("users")]
    public class UserController
        (IMapper mapper) 
        : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [HttpGet("profile")]
        public async Task<ActionResult<UserDetails>> GetProfile()
        {
            var query = new GetProfileQuery { Id = UserId };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
        {
            var command = _mapper.Map<UpdateProfileCommand>(dto);
            command.Id = UserId;
            await Mediator.Send(command);

            return Ok();
        }
    }
}
