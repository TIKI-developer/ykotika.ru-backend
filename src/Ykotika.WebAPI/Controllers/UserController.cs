using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
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
        public async Task<ActionResult<UserDetails>>
            GetProfile()
        {
            var query = new GetProfileQuery { Id = UserId };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPut("profile")]
        public async Task<IActionResult>
            UpdateProfile([FromBody] UpdateProfileDto dto)
        {
            var command = _mapper.Map<UpdateProfileCommand>(dto);
            command.Id = UserId;
            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<UserList>>
            Get()
        {
            var query = new GetUserListQuery();
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserList>>
            Get(Guid id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult>
            ChangePermissions(Guid id, [FromBody] UpdateUserPermissionsDto dto)
        {
            var command = _mapper.Map<UpdateUserPermissionsCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
    }
}
